using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class Cashier : Person
    {
        //Atributs
        private DateTime _joiningDate;

        //Propietats

        /// <summary>
        /// Hem de calcular els dias que porta de contratació
        /// Per obtenir el rating fem una multiplicacio de daysOfService * (totalventes * 10%)
        /// </summary>
        /// <returns></returns>
        public override double GetRating
        {
            get
            {
                int daysOfService = (DateTime.Now - this._joiningDate).Days;
                return daysOfService + (this._totalInvoiced * 0.10);
            }
        }

        /// <summary>
        /// Calcula els anys de servei de Cashier fent un caclul de la data de contratació - la data actual
        /// </summary>
        public int YearsOfService
        {
            get
            {
                return DateTime.Now.Year - this._joiningDate.Year;
            }
        }
        
        //Constructor
        /// <summary>
        /// Constuctor que rep els parametres de Persona
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullName"></param>
        /// <param name="joiningDate"></param>
        public Cashier(string id, string fullName, DateTime joiningDate) : base(id, fullName)
        {
            this._joiningDate = joiningDate;
        }
        
        //METODES
        /// <summary>
        /// Afegeix els punts depenen els anys de servei
        /// Es calcula fent AnysDeServei +1 i multiplicant-lo per pointsToAdd
        /// </summary>
        /// <param name="pointsToAdd"></param>
        public override void AddPoints(int pointsToAdd)
        {
            this._point += (this.YearsOfService + 1) * pointsToAdd;
        }
        
        public override string ToString()
        {
            return $"DNI/NIE-->{this._id} NOM-->{this._fullName} RATING --> {this.GetRating} ANTIGUITAT -->{this.YearsOfService} VENDES-->{this._totalInvoiced} PUNTS-->{this._point} {base.ToString()}";
        }
    }
}
