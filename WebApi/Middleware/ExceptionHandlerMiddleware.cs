using Core.Models;
using System.Net;
using System.Text.Json;

namespace WebApi.Middleware;

public class ExceptionHandlerMiddleware
{

    private readonly RequestDelegate _next; //guarda el "camino" a seguir después de que el guardián hace su trabajo.
    private const string _contentType = "application/json"; //le dice al guardián que si algo va mal, debe responder en un lenguaje que la computadora entiende


  //  Este constructor le da al guardián el próximo paso(next) para seguir después de hacer su trabajo.
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    //Invoke es donde el guardián hace su trabajo. HttpContext es como un paquete con información sobre lo que está pasando en ese momento.
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (InvalidOperationException sqlEx) 
        {
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            context.Response.ContentType = _contentType;

            /*
             * Se crea un objeto ErrorModel con un campo Message que contiene el texto del error.
             El Message intenta obtener el mensaje más específico que tiene la excepción sqlEx. Para eso, busca en InnerException
            (esto es como mirar dentro de la excepción para encontrar más detalles). 
            Si InnerException no tiene detalles específicos, muestra un mensaje general: "ocurrio un error en la bd".
            */
            var error = new ErrorModel
            {
                Message = sqlEx.InnerException?.InnerException?.Message ?? "ocurrio un error en la bd",
            };

            var errorJson = JsonSerializer.Serialize(error);
            await context.Response.WriteAsync(errorJson);

        }

        catch (Exception ex) 
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = _contentType;

            var error = new ErrorModel
            {
                Message = ex.Message,
            };

            var errorJson = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(errorJson);
        }

    }
}
