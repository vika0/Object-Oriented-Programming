using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<BAZINE KLASE>
namespace Lab3.Step1
{
    ///<bazine klasė Animal, kurioje patalpinami kačių ir šunų bendriniai laukai ir metodai
    ///bazine klase Animal turi konstruktoriu, kuris inicializuos tik bendrus (aukščiau nurodytus) laukus>
   abstract class Animal
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public DateTime VaccinationDate { get; set; }

        public Animal(string name, string breed, string owner, string phone, DateTime vaccinationDate)
        {
            Name = name;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
        }
        abstract public bool isVaccinationExpired();
        ///<abstraktus metodasnes kačių vakcinavimas įvertinamas mėn.(t.y. šis metodas skiriasi katėms ir šunims, kuriems vakcinavimas įvertinamas metais)
       /// Vaikinės šunų ir kačių klasės privalės realizuoti šį metodą. 
        ///Kadangi klasė  Animal turės  abstaktų metodą, klasė 
        ///taip pat turės būti paskelbta  abstrakčia>
     
    }
}