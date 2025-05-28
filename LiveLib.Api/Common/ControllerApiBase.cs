using LiveLib.Application.Commom.ResultWrapper;
using Microsoft.AspNetCore.Mvc;

namespace LiveLib.Api.Common
{
    public class ControllerApiBase : ControllerBase
    {
        [NonAction]
        public IActionResult ToActionResult(Result result)
        {
            if (result.IsSuccess)
            {
                return result.SuccessInfo!.Code switch
                {
                    SuccessCode.Ok => Ok(result.SuccessInfo.Message),
                    SuccessCode.Created => Created(),
                    SuccessCode.Accepted => Accepted(result.SuccessInfo.Message),
                    SuccessCode.NoContent => NoContent(),
                    _ => Ok()
                };
            }

            return result.ErrorInfo!.Code switch
            {
                ErrorCode.Conflict => Conflict(result.ErrorInfo.Message),
                ErrorCode.Forbiden => Forbid(result.ErrorInfo.Message),
                ErrorCode.NotFound => NotFound(result.ErrorInfo.Message),
                ErrorCode.ServerError => Problem(result.ErrorInfo.Message),
                ErrorCode.BadRequest => BadRequest(result.ErrorInfo.Message),
                _ => StatusCode(500, result.ErrorInfo.Message)
            };

        }

        [NonAction]
        public IActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                if (result.Value is null)
                {
                    return NoContent();
                }
                return result.SuccessInfo!.Code switch
                {
                    SuccessCode.Ok => Ok(result.Value),
                    SuccessCode.NoContent => NoContent(),
                    SuccessCode.Accepted => Accepted(result.SuccessInfo.Message),
                    SuccessCode.Created => Created(),
                    _ => Ok(result.Value)
                };
            }

            return result.ErrorInfo!.Code switch
            {
                ErrorCode.NotFound => NotFound(result.ErrorInfo.Message),
                ErrorCode.Conflict => Conflict(result.ErrorInfo.Message),
                ErrorCode.ServerError => Problem(result.ErrorInfo.Message),
                ErrorCode.Forbiden => Forbid(result.ErrorInfo.Message),
                ErrorCode.BadRequest => BadRequest(result.ErrorInfo.Message),
                _ => StatusCode(500, result.ErrorInfo.Message)
            };
        }
    }
}
