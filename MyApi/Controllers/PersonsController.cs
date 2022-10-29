using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MyApi.Models;

namespace MyApi.Controllers
{
    public class PersonsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Persons
        public IQueryable<Persons> GetPersons()
        {
            return db.Persons;
        }

        // GET: api/Persons/5
        [ResponseType(typeof(List<ModelPerson>))]
        public IHttpActionResult GetPersons(int id)
        {
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return NotFound();
            }

            return Ok(db.Persons.ToList().ConvertAll(x => new ModelPerson(x)));
        }

        // PUT: api/Persons/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersons(int id, Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persons.id_person)
            {
                return BadRequest();
            }

            db.Entry(persons).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Persons
        [ResponseType(typeof(Persons))]
        public IHttpActionResult PostPersons(Persons persons)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Persons.Add(persons);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = persons.id_person }, persons);
        }

        // DELETE: api/Persons/5
        [ResponseType(typeof(Persons))]
        public IHttpActionResult DeletePersons(int id)
        {
            Persons persons = db.Persons.Find(id);
            if (persons == null)
            {
                return NotFound();
            }

            db.Persons.Remove(persons);
            db.SaveChanges();

            return Ok(persons);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonsExists(int id)
        {
            return db.Persons.Count(e => e.id_person == id) > 0;
        }
    }
}