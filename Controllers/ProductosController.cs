using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using systemMantenimiento.Models;
using systemMantenimiento.Util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace systemMantenimiento.Controllers
{
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        // we want json  not those weird string ? Ohh Microsoft ?
        [HttpGet]
        public ActionResult Get()
        {
            ProductoRepository productoRepository = new();

            List<ProductoModel> data = productoRepository.Read();

            return Ok(new { data });
        }
        // what if we need post ? we don't some idiot playing with mvc / / / / ?
        [HttpPost]
        public ActionResult Post()
        {
            var status = false;
            // wait // anybody tell this before ? 
            var mode = Request.Form["mode"];

            var producto = Request.Form["producto"];
            var costo = Request.Form["costo"];
            var serie = Request.Form["serie"];
            var stock = Request.Form["stock"];
            var estado = Request.Form["estado"];
            var productoId = Request.Form["productoId"];

            ProductoRepository productoRepository = new();

            List<ProductoModel> data = new();

            var code = "";
            // but we think something missing .. what ya ? 
            switch (mode)
            {
                case "create":
                    // for ide like php storm maybe they will angry ? wher's my catch ?
                    try
                    {
                        productoRepository.Create(producto, Convert.ToDouble(costo), Convert.ToInt32(serie), Convert.ToInt32(stock), estado);

                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                case "read":
                    try
                    {
                        data = productoRepository.Read();
                        code = ((int)ReturnCode.CREATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }

                    break;
                case "update":
                    try
                    {
                        productoRepository.Update(producto, Convert.ToDouble(costo), Convert.ToInt32(serie), Convert.ToInt32(stock), estado, Convert.ToInt32(productoId));
                        code = ((int)ReturnCode.UPDATE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                case "delete":
                    try
                    {
                        productoRepository.Delete(Convert.ToInt32(productoId));

                        code = ((int)ReturnCode.DELETE_SUCCESS).ToString();
                        status = true;

                    }
                    catch (Exception ex)
                    {
                        code = ex.Message;

                    }
                    break;
                default:
                    // do remmeber hacker always exist anywhere ?
                    code = ((int)ReturnCode.ACCESS_DENIED_NO_MODE).ToString();
                    break;
            }

            return Ok(new { status, code, data });
        }
    }
}

