using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyApi.Models
{
    public class ModelPerson
    {
        public int id { get; set; }
        public string fname { get; set; }    
        public string lname { get; set; }
       public string img { get; set; }

        public ModelPerson(Persons persons)
        {
            id = persons.id_person;
            fname = persons.fname;
            lname = persons.lname;
            img = persons.image;
            
    }
            
    }
}