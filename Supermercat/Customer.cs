using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public class Customer : Person
    {
        private int? _fidelity_card;
        public Customer(string id, string fullName, int? fidelityCard) : base(id, fullName)
        {
            this._fidelity_card = fidelityCard;
        }
        public override double GetRating()
        {

            return _totalInvoiced * 0.02;
        }

        public override void AddPoints (int point)
        {
            _point = _point + point;
        }

        public override string ToString()
        {
            return $"DNI/NIE-->{this._id} NOM-->{this._fullName}{base.ToString()}";
        }

    }
}