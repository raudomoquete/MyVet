using Microsoft.AspNetCore.Mvc.Rendering;
using MyVet.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _dataContext;

        public CombosHelper(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        //aqui tomo la coleccion de tipos de mascota y la convierto en una lista
        public IEnumerable<SelectListItem> GetComboPetTypes()
        {
            //var list = new List<SelectListItem>();
            //foreach(var petType in _dataContext.PetTypes)
            //{                                                 Esta es la manera antigua
            //    list.Add(new SelectListItem
            //    {
            //        Text = petType.Name,
            //        Value = $"{petType.Id}"
            //    });
            //}

            // Esta es la nueva manera de hacerlo
            var list = _dataContext.PetTypes.Select(pt => new SelectListItem 
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            }).OrderBy(pt => pt.Text)
                .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select a pet type...]",
                Value = "0"
            });
            return list;
        }

        public IEnumerable<SelectListItem> GetComboServiceTypes()
        {
            var list = _dataContext.ServiceTypes.Select(pt => new SelectListItem
            {
                Text = pt.Name,
                Value = $"{pt.Id}"
            }).OrderBy(pt => pt.Text)
               .ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "[Select a Service type...]",
                Value = "0"
            });
            return list;
        }
    }
}
