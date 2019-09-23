using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;
using System.Net;


namespace DEV.API.App.Domain.Core.Services
{
    public abstract class BaseControllerCore : ControllerBase
    {
        private readonly DomainNotificationHandler _messageHandler;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        protected BaseControllerCore(INotificationHandler<DomainNotification> notification)
        {
            _messageHandler = (DomainNotificationHandler)notification;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected bool HasNotification()
        {
            return _messageHandler.HasNotifications();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IActionResult NotificationBusiness()
        {
            var notifications = _messageHandler.GetNotifications();
            var domainNotificationType = notifications?.FirstOrDefault()?.DomainNotificationType;


            return new JsonResult(new ExceptionResponse(notifications?.ToList()))
            {
                StatusCode = (int?)domainNotificationType
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected IActionResult HttpResponse(object response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (_messageHandler.HasNotifications())
            {
                var notifications = _messageHandler.GetNotifications();

                var domainNotificationType = notifications?.FirstOrDefault()?.DomainNotificationType;
                if (domainNotificationType != null)
                {
                    return new JsonResult(new ExceptionResponse(notifications.ToList()))
                    {
                        StatusCode = (int)domainNotificationType
                    };
                }
            }

            return new JsonResult(response)
            {
                StatusCode = (int)statusCode
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult OkOrNotFound<T>(T result = default(T))
        {

            if (!HasNotification())
            {
                if (result != null)
                {
                    if (result is bool)
                        return Ok();

                    return Ok(new Result<T>
                    {
                        Success = true,
                        Data = result
                    });
                }

                return NotFound();

            }

            return NotificationBusiness();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult OkOrNoContent<T>(T result = default(T))
        {
            if (!HasNotification() && result != null)
            {
                if (result is IEnumerable)
                {
                    if (((ICollection)result).Count > 0)
                    {
                        return Ok(new Result<T>
                        {
                            Success = true,
                            Data = result
                        });
                    }

                    return NoContent();
                }

                return Ok(new Result<T>
                {
                    Success = true,
                    Data = result
                });
            }

            if (!HasNotification() && result == null)
            {
                return NoContent();
            }

            return NotificationBusiness();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult AcceptedContent<T>(T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Accepted(new Result<T>
                    {
                        Success = true,
                        Data = result
                    });

                return NotFound();
            }

            return NotificationBusiness();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult AcceptedOrContent<T>(T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Accepted(new Result<T>
                    {
                        Success = true,
                        Data = result
                    });

                return Accepted();
            }

            return NotificationBusiness();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rota"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected IActionResult CreatedContent<T>(string rota, T result = default(T))
        {
            if (!HasNotification())
            {
                if (result != null)
                    return Created(rota, new Result<T>
                    {
                        Success = true,
                        Data = result
                    });

                return Created(rota, new Result
                {
                    Success = true
                });
            }

            return NotificationBusiness();
        }
    }
    public class Result
    {
        public bool Success { get; set; }
    }
    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}
