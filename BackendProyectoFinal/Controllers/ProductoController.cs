﻿using Microsoft.AspNetCore.Mvc;

namespace BackendProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            Console.WriteLine(id);
            return Ok();
        }

        [HttpPost]
        public ActionResult Add()
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Update()
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok();
        }
    }
}
