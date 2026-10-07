using Newtonsoft.Json;
using POOI_T2_SALAZAR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_T2_SALAZAR.Controllers
{
    public class AlumnoController : Controller
    {
        //ruta para el archivo
        string ruta = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data"), "alumnos.json");

        //listamos en un index
        public ActionResult Index()
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            return View(lista);
        }

        //agregamos
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        // POST:
        [HttpPost]
        public ActionResult Agregar(Alumno obj)
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            //valimdamos si el DNI existe
            foreach (var item in lista)
            {
                if (item.dni == obj.dni)
                {
                    ViewBag.Mensaje = "DNI REGISTRADO, INGRESE OTRO";
                    return View(obj);
                }
            }

            //agregamos el nuevo alumno
            lista.Add(obj);

            //guardamos y serializamos
            string jsonNuevo = JsonConvert.SerializeObject(lista);
            System.IO.File.WriteAllText(ruta, jsonNuevo);

            return RedirectToAction("Index");
        }

        // GET: Alumno/Delete
        public ActionResult Eliminar(string dni)
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            //buscamos y elimnamos
            lista.RemoveAll(a => a.dni == dni);

            string jsonActualizado = JsonConvert.SerializeObject(lista);
            System.IO.File.WriteAllText(ruta, jsonActualizado);

            return RedirectToAction("Index");
        }
        // GET: Details
        public ActionResult Detalles(string dni)
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            Alumno obj = null;
            foreach (var item in lista)
            {
                if (item.dni == dni)
                {
                    obj = item;
                    break;
                }
            }

            if (obj == null)
            {
                return HttpNotFound();
            }

            return View(obj);
        }

        // GET: Actualizar
        [HttpGet]
        public ActionResult Actualizar(string dni)
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            Alumno obj = null;
            foreach (var item in lista)
            {
                if (item.dni == dni)
                {
                    obj = item;
                    break;
                }
            }

            if (obj == null)
            {
                return HttpNotFound();
            }

            return View(obj);
        }

        // POST: Actualizar
        [HttpPost]
        public ActionResult Actualizar(Alumno obj)
        {
            List<Alumno> lista = new List<Alumno>();

            if (System.IO.File.Exists(ruta))
            {
                string json = System.IO.File.ReadAllText(ruta);
                lista = JsonConvert.DeserializeObject<List<Alumno>>(json) ?? new List<Alumno>();
            }

            //actualizamos los datos
            foreach (var item in lista)
            {
                if (item.dni == obj.dni)
                {
                    item.nombres = obj.nombres;
                    item.apellidos = obj.apellidos;
                    item.carrera = obj.carrera;
                    item.ciclo = obj.ciclo;
                    break;
                }
            }

            //guardamos y serializanos
            string jsonActualizado = JsonConvert.SerializeObject(lista);
            System.IO.File.WriteAllText(ruta, jsonActualizado);

            return RedirectToAction("Index");
        }
    }
}