﻿using System;
namespace Lab1.Step3
{
    class Dog
    {
        private static int VaccinationDuration = 1;
        public string Name { get; set; }
        public int ChipId { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public DateTime VaccinationDate { get; set; }
        public bool Aggressive { get; set; }
        public Dog()
        {
        }
        public Dog(string name, int chipId, double weight, int age, string breed,
       string owner, string phone, DateTime vaccinationDate, bool aggressive)
        {
            Name = name;
            ChipId = chipId;
            Weight = weight;
            Age = age;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
            Aggressive = aggressive;
        }
        public bool isVaccinationExpired()
        {
            return
           VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) < 0;
        }
    }
}


