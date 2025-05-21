using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class Customer : Person
    {
        //ATRIBUT
        private int? _fidelity_card;

        /// <summary>
        /// Constuctor que rep els parametres de Persona
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullName"></param>
        /// <param name="fidelityCard"></param>
        public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
        {
            this._fidelity_card = fidelityCard;
        }
        /// <summary>
        /// LA propietat calcula el rating del costumer acord la quantitat de compres
        /// El rating es el 2% de les compres
        /// </summary>
        /// <returns></returns>
        public override double GetRating
        {
            get
            {
                return this._totalInvoiced * 0.02;
            }
        }

        /// <summary>
        /// Afegeix els punts
        /// </summary>
        /// <param name="point"></param>
        public override void AddPoints (int point)
        {
            if (this._fidelity_card != null) { this._point = this._point + point; }
        }

        public override string ToString()
        {
            return $"DNI/NIE->{this._id} NOM->{this._fullName} RATING -> {this.GetRating} VENDES ->{this._totalInvoiced}  PUNTS->{this._point} {base.ToString()}";
        }

        /// <summary>
        /// Genera un hashcode a partir de tots els atributs
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this._id, this._fullName);
        }

        /// <summary>
        /// Filtra dos elements per a mirar si son comparables coma  Customer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            bool iguals = false;
            if (ReferenceEquals(null, obj)) iguals = false;
            else if (ReferenceEquals(this, obj)) iguals = true;
            else if (obj.GetType() != this.GetType()) iguals = false;
            else iguals = Equals((Customer)obj);
            return iguals;
        }

        /// <summary>
        /// Retorna true si els dos customers tenen el mateix id
        /// </summary>
        /// <param name="other">objecte a comparar</param>
        /// <returns>true si els dos id son iguals</returns>
        public bool Equals(Customer other)
        { 
            return this._id.Equals(other._id);
        }
    }
}
