using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercat
{
    public abstract class Person : IComparable<Person>
    {
        // Atributs
        public string _id;
        public string _fullName;
        public int _point;
        public double _totalInvoiced;
        private bool active = false;

        // Propietats
        /// <summary>
        /// Obte el nom complert de persona
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public abstract double GetRating { get; }


        // Constructors
        /// <summary>
        /// Crea una persona inicializando los atributos asociados a los parámetros, 
        /// y estableciendo `totalInvoiced` en 0 y `active` en `false`.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullName"></param>
        /// <param name="points"></param>
        protected Person(string id, string fullName, int points)
        {
            this._id = id;
            this._fullName = fullName;
            this._point = points;
            this._totalInvoiced = 0;
            this.active = false;
        }

        /// <summary>
        /// Crea una persona amb 0 points
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fullName"></param>
        protected Person(string id, string fullName)
        {
            this._id = id;
            this._fullName = fullName;
            this._point = 0;
            this._totalInvoiced = 0;
        }

        /// <summary>
        /// Método útil para sumar el importe facturado en una operación de compra 
        /// (válido para clientes y cajeros)
        /// </summary>
        /// <param name="amount"></param>
        public void AddInvoiceAmount(double amount)
        {
            _totalInvoiced = _totalInvoiced + amount;
        }

        public virtual void AddPoints(int points)
        {
            _point += points;
        }

        /// <summary>
        /// Mostra S o N dependiendo del atributo Active 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (active == false)
            {
                return "DISPONIBLE -> S";
            }
            else
            {
                return "DISPONIBLE -> N";
            }
        }
        /// <summary>
        /// Compara les persones i les ordena depenen el seu ranking
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Person? other)
        {
            return other.GetRating().CompareTo(this.GetRating());
        }
    }
}