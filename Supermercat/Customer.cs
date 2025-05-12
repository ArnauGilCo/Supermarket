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
                return _totalInvoiced * 0.02;
            }
        }

        /// <summary>
        /// Afegeix els punts
        /// </summary>
        /// <param name="point"></param>
        public override void AddPoints (int point)
        {
            _point = _point + point;
        }

        public override string ToString()
        {
            return $"DNI/NIE-->{this._id} NOM-->{this._fullName} RATING --> {this.GetRating()} VENDES->{this._totalInvoiced}  PUNTS-->{this._point} {base.ToString()}";
        }
    }
}
