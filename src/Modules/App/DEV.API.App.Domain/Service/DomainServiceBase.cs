using DEV.API.App.Domain.Core.UoW;

namespace DEV.API.App.Domain.Interfaces.Entities.Base
{
    public class DomainServiceBase<TEntity, TKey, TIUnitOfWork> : UnitOfWorkBase<TIUnitOfWork>, IDomainService<TEntity, TKey>
         where TEntity : Entidade<TKey>
         where TKey : struct
        where TIUnitOfWork : IUnitOfWorkBase
    {
        protected readonly IRepositorio<TEntity, TKey> _repositorio;
        protected readonly TIUnitOfWork _unitOfWork;
        private readonly IRepositorioLog<TKey> _repositorioLog;
        private readonly UsuarioLogado.Modelo.UsuarioLogado _usuarioLogado;

        public DominioServicoBase(
            IRepositorio<TEntity, TKey> repositorio,
            TIUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> messageHandler,
            IRepositorioLog<TKey> repositorioLog, UsuarioLogado.Modelo.UsuarioLogado usuarioLogado) : base(unitOfWork, messageHandler)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
            _repositorioLog = repositorioLog;
            _usuarioLogado = usuarioLogado;
        }


        public virtual async Task<TEntity> AlterarAsync(TKey id, TEntity obj)
        {
            using (_unitOfWork.BeginTransaction())
            {
                var objetoAnterior = await _repositorio.SelecionarPorIdAsync(id);
                await GravarLog<TEntity, TKey>(obj, objetoAnterior, "AlterarAsync");
                var retorno = await _repositorio.AlterarAsync(id, obj);
                Commit();
                return retorno;
            }
        }
        public virtual async Task DeletarAsync(TEntity entity)
        {
            using (_unitOfWork.BeginTransaction())
            {
                await GravarLog<TEntity, TKey>(null, entity, "DeletarAsync");
                await _repositorio.DeletarAsync(entity);
                Commit();
            }
        }
        public virtual async Task InserirAsync(TEntity entity)
        {
            using (_unitOfWork.BeginTransaction())
            {
                await _repositorio.InserirAsync(entity);
                Commit();
                await GravarLog<TEntity, TKey>(entity, null, "InserirAsync");
            }
        }
        public virtual async Task<IEnumerable<TEntity>> SelecionarAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _repositorio.SelecionarAsync(where);
        }
        public virtual async Task<TEntity> SelecionarPorIdAsync(TKey id)
        {
            return await _repositorio.SelecionarPorIdAsync(id);
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task GravarLog<TEntity, TKey>(TEntity entityAtual, TEntity entityAntes, string operacao)
            where TEntity : Entidade<TKey>
           where TKey : struct
        {
            if (_repositorio.Context == null) return;

            var entityDefault = entityAtual == null ? entityAntes : entityAtual;
            var type = entityDefault.GetType();
            var schemaAnnotation = _repositorio.Context.Model.FindEntityType(type).GetAnnotations();

            var registroAtual = entityAtual != null ? JsonConvert.SerializeObject(entityAtual, Formatting.Indented, Settings<TEntity, TKey>()) : "";
            var registroAntes = entityAntes != null ? JsonConvert.SerializeObject(entityAntes, Formatting.Indented, Settings<TEntity, TKey>()) : "";

            var schema = schemaAnnotation.FirstOrDefault(x => x.Name == "Relational:Schema");
            var table = schemaAnnotation.FirstOrDefault(x => x.Name == "Relational:TableName");

            var schemaName = "";
            var tableName = "";

            if (schema != null)
                schemaName = string.IsNullOrWhiteSpace(schema.Value.ToString()) ? "" : schema.Value.ToString();

            if (table != null)
                tableName = string.IsNullOrWhiteSpace(table.Value.ToString()) ? "" : table.Value.ToString();

            var log = new Log<TKey>(
                entityDefault.Id,
                type.ToString(),
                _usuarioLogado?.Id,
                _usuarioLogado?.Login,
                _usuarioLogado?.IdPessoa,
                operacao,
                schemaName,
                tableName,
                registroAtual,
                registroAntes
            );

            await _repositorioLog.InserirLogAsync(log);
        }

        [ExcludeFromCodeCoverage]
        private static JsonSerializerSettings Settings<TEntity, TKey>() where TEntity : Entidade<TKey> where TKey : struct
        {
            return new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        [ExcludeFromCodeCoverage]
        public async Task GravarLogAlterar<TEntity, TKey>(TKey id, TEntity entityAtual)
            where TEntity : Entidade<TKey>
            where TKey : struct
        {
            if (_repositorio.Context == null) return;


            var entityAnterior = _repositorio.Context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
            await GravarLog<TEntity, TKey>(entityAtual, entityAnterior, "AlterarAsync");
        }

        [ExcludeFromCodeCoverage]
        public async Task GravarLogInserir<TEntity, TKey>(TEntity entityAtual)
            where TEntity : Entidade<TKey>
            where TKey : struct
        {
            if (_repositorio.Context == null) return;

            await GravarLog<TEntity, TKey>(entityAtual, null, "InserirAsync");
        }

        [ExcludeFromCodeCoverage]
        public async Task GravarLogDeletar<TEntity, TKey>(TKey id)
            where TEntity : Entidade<TKey>
            where TKey : struct
        {
            if (_repositorio.Context == null) return;

            var entityAnterior = _repositorio.Context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
            await GravarLog<TEntity, TKey>(null, entityAnterior, "DeletarAsync");
        }
    }


}
