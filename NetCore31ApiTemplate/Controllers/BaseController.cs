﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace NetCore31ApiTemplate.Controllers
{
    public class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ExecuteAndMapToActionResult<T>(Func<Task<T>> request)
        {
            try
            {
                var response = await request.Invoke();

                return response switch
                {
                    null => throw new NullReferenceException(),

                    Exception errorResponse => throw errorResponse,

                    _ => Ok(response)
                };
            }
            catch (NullReferenceException ex)
            {
                Log.Error(ex, ex.Message);
                return NotFound($"{request.Method.Name} returned null");
            }
            catch (ValidationException ex)
            {
                Log.Error(ex, $"Bad Request: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return Problem(ex.Message);
            }
        }
    }
}