using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class QuoteCalculate
    {
        Insuree insuree = new Insuree();
        decimal baseQuote = 50m;
        DateTime currentDate = DateTime.Now;
        public decimal monthlyTotal;
        public decimal ageFeeCalculation(DateTime birthDay)
        {

            if (birthDay >= this.currentDate.AddYears(-18))
            {
                this.monthlyTotal = baseQuote + 100m;

            }
            else if (birthDay <= this.currentDate.AddYears(-19) && birthDay >= this.currentDate.AddYears(-25))
            {
                this.monthlyTotal = baseQuote + 50m;

            }
            else if (birthDay <= this.currentDate.AddYears(-26))
            {
                this.monthlyTotal = baseQuote + 25m;

            }
            return this.monthlyTotal;
        }

        public decimal checkVehicle(int carYear, string carModel, string carMake)
        {
            if (carYear < 2000 || carYear > 2015)
            {
                this.monthlyTotal += 25m; //this is a short hand of this.monthlyTotal = this.monthlyTotal + 25m;
            }
            else if (carMake == "Porche" && carModel == "911 Carrera")
            {
                this.monthlyTotal += 50m;
            }
            else if (carMake == "Porche")
            {
                this.monthlyTotal += 25m;
            }
            return this.monthlyTotal;
        }

        public decimal speedTicketCalc(int speedingTicket)
        {
            this.monthlyTotal += (speedingTicket * 10);
           
            return this.monthlyTotal;
        }

        public decimal calcDUI(bool myDUI)
        {
            if (myDUI)
            {
                this.monthlyTotal += (this.monthlyTotal * .25m);
            }
            return this.monthlyTotal;
        }

        public decimal fullCoverageCalc(bool FullConverage)
        {
            if (FullConverage)
            {
                this.monthlyTotal += (this.monthlyTotal * .50m);
            }
            return this.monthlyTotal;
        }
   
    }
}