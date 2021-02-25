using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WSSOAP
{
    /// <summary>
    /// Descripción breve de WSPersonas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSPersonas : System.Web.Services.WebService
    {
        private personasEntities db = new personasEntities();
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        [WebMethod]
        public Persona seleccionarPersona(string nif)
        {
            try
            {
                Persona p = db.Personas.Where(x => x.nif == nif).First();
                return p;
            }
            catch
            {
                return null;
            }
        }

        [WebMethod]
        public List<Persona> seleccionarTodasPersonas()
        {
            return db.Personas.Select(x => x).ToList();
        }
        [WebMethod]
        public bool borrarPersona(string nif)
        {
            try
            {

                Persona p = db.Personas.Where(x => x.nif == nif).First();
                db.Personas.Remove(p);
                db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        [WebMethod]
        public Persona añadirPersona(string nombre, string apellido, string nif, string ciudad, string direccion, string estadocivil, string provincia, string sexo)
        {
            //Esto se deberia hacer solo pasando un cliente y no todos sus datos pero para probar lo dejo asi
            try
            {
                Persona p = new Persona();
                p.nombre = nombre;
                p.apellido = apellido;
                p.ciudad = ciudad;
                p.direccion = direccion;
                p.estadocivil = estadocivil;
                p.nif = nif;
                p.provicia = provincia;
                p.sexo = sexo;

                db.Personas.Add(p);
                db.Entry(p).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return p;
            }
            catch
            {
                return null;
            }
        }
        [WebMethod]
        public Persona modificarPersona(string nombre, string apellido, string nif, string ciudad, string direccion, string estadocivil, string provincia, string sexo)
        {
            try
            {
                Persona p = db.Personas.Where(x => x.nif == nif).First();
                if(nombre!="")p.nombre = nombre;
                if (apellido != "") p.apellido = apellido;
                if (ciudad != "") p.ciudad = ciudad;
                if (direccion != "") p.direccion = direccion;
                if (estadocivil != "") p.estadocivil = estadocivil;
                if (provincia != "") p.provicia = provincia;
                if (sexo != "") p.sexo = sexo;
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return p;
            }
            catch
            {
                return null;
            }

        }
    }
}
