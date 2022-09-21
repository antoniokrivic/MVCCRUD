using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.DAL
{
    public class VehicleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VehicleContext>
    {
        protected override void Seed(VehicleContext context)
        {
            var VehicleMakeList = new List<VehicleMake>
            {
                VehicleMakeInsert(Guid.NewGuid(), "BMW", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Porsche", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Ford", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Honda", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Volkswagen", "VW"),
                VehicleMakeInsert(Guid.NewGuid(), "Subaru", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Jeep", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Toyota", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Ford", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Honda", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Audi", "/"),
                VehicleMakeInsert(Guid.NewGuid(), "Opel", "/")
            };
            VehicleMakeList.ForEach(x => context.VehicleMakes.Add(x));
            context.SaveChanges();
            
            var VehicleModelList = new List<VehicleModel> 
            {
                VehicleModelInsert(Guid.NewGuid(), "M3", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Panamera", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Focus", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Civic", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Passat B3", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Impreza", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Grand Cherokee", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Highlander", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Escape", "/"),
                VehicleModelInsert(Guid.NewGuid(), "CR-V", "/"),
                VehicleModelInsert(Guid.NewGuid(), "A4", "/"),
                VehicleModelInsert(Guid.NewGuid(), "Corsa", "/")
            };
            VehicleModelList.ForEach(x => context.VehicleModel.Add(x));
            context.SaveChanges();
            base.Seed(context);
        }

        public VehicleMake VehicleMakeInsert(Guid vmakeid, string name, string abrv)
        {
            return new VehicleMake { VMakeID = vmakeid ,Name = name, Abrv = abrv };
        }
        public VehicleModel VehicleModelInsert(Guid vmakeid, string model, string abrv)
        {
            return new VehicleModel {VMakeID = vmakeid, Model = model, Abrv = abrv };
        }
    }
}
