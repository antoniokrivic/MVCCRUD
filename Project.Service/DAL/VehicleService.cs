﻿using Project.Service.Interfaces;
using Project.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using PagedList;
using System.Collections;
using Project.Service.ViewModels;
using AutoMapper;

namespace Project.Service.DAL
{
    public class VehicleService : IVehicleService
    {
        private static VehicleService Vehicleservice;
        private readonly VehicleContext Db;
        private VehicleMakeViewModel VehicleMake;
        private VehicleModelViewModel VehicleModel;
        private int PageSize = 4;
        IPagedList VehiclesList;

        private VehicleService(){
            Db = new VehicleContext();
        }
        public static VehicleService GetInstance()
        {
            if (Vehicleservice == null)
            {
                Vehicleservice = new VehicleService();
                return Vehicleservice;
            }
            else return Vehicleservice;
        }
        public void CreateVehicleMaker(VehicleMakeViewModel vehicleMaker)
        {

            Db.VehicleMakes.Add(Mapper.Map<VehicleMake>(vehicleMaker));
            Db.SaveChanges();
        }

        public void CreateVehicleModel(VehicleModelViewModel vehicleModel)
        {
            Db.VehicleModel.Add(Mapper.Map<VehicleModel>(vehicleModel));
            Db.SaveChanges();
        }

        public VehicleMakeViewModel FindVehicleMaker(Guid? id)
        {
            return Mapper.Map<VehicleMake, VehicleMakeViewModel>(Db.VehicleMakes.Find(id));
        }

        public VehicleModelViewModel FindVehicleModel(Guid? id)
        {
            return Mapper.Map<VehicleModel, VehicleModelViewModel>(Db.VehicleModel.Find(id));
        }

        public void DeleteVehicleMaker(Guid? id)
        {
            VehicleMake vehicleMake = Db.VehicleMakes.Find(id);
            Db.VehicleMakes.Remove(vehicleMake);
            Db.SaveChanges();
        }

        public void DeleteVehicleModel(Guid? id)
        {
            VehicleModel vehicleModel = Db.VehicleModel.Find(id);
            Db.VehicleModel.Remove(vehicleModel);
            Db.SaveChanges();
        }

        public void UpdateVehicleMaker(VehicleMakeViewModel vehicleMaker)
        {
            Db.Set<VehicleMake>().AddOrUpdate(Mapper.Map<VehicleMake>(vehicleMaker));
            Db.SaveChanges();
        }

        public void UpdateVehicleModel(VehicleModelViewModel vehicleModel)
        {
            Db.Set<VehicleModel>().AddOrUpdate(Mapper.Map<VehicleModel>(vehicleModel));
            Db.SaveChanges();
        }

        public IPagedList SearchSortVehicleModel(string Condition_sort, int? page, string Condition_search, string currentFilter)
        {
            var vehicles = Db.VehicleModel.AsQueryable();

            if (Condition_search == null || Condition_search.Equals(""))
            {
                Condition_search = currentFilter;
                vehicles = Db.VehicleModel.AsQueryable();
            }
            else
            {
                page = 1;
                vehicles = Db.VehicleModel.Where(x => x.VehicleMake.Name.Contains(Condition_search) || x.Model.Contains(Condition_search));
            }

            switch (Condition_sort)
            {
                case "Name":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderBy(x => x.VehicleMake.Name)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Name_desc":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderByDescending(x => x.VehicleMake.Name)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Model":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderBy(x => x.Model)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Model_desc":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderByDescending(x => x.Model)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Abrv":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderBy(x => x.Abrv)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Abrv_desc":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderByDescending(x => x.Abrv)).ToPagedList(page ?? 1, PageSize);
                    break;
                default:
                    VehiclesList = Mapper.Map<IEnumerable<VehicleModelViewModel>>(vehicles.OrderBy(x => x.Model)).ToPagedList(page ?? 1, PageSize);
                    break;
            }
            return VehiclesList;
        }
        public IPagedList SearchSortVehicleMaker(string Condition_sort, int? page, string Condition_search, string currentFilter)
        {
            var Vehicles = Db.VehicleMakes.AsQueryable();

            if (Condition_search == null || Condition_search.Equals(""))
            {
                Condition_search = currentFilter;
                Vehicles = Db.VehicleMakes.AsQueryable();
            }
            else
            {
                page = 1;
                Vehicles = Db.VehicleMakes.Where(x => x.Name.Contains(Condition_search));
            }
            

            switch (Condition_sort)
            {
                case "Name":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(Vehicles.OrderBy(x => x.Name)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Name_desc":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(Vehicles.OrderByDescending(x => x.Name)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Abrv":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(Vehicles.OrderBy(x => x.Abrv)).ToPagedList(page ?? 1, PageSize);
                    break;
                case "Abrv_desc":
                    VehiclesList = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(Vehicles.OrderByDescending(x => x.Abrv)).ToPagedList(page ?? 1, PageSize);
                    break;
                default:
                    VehiclesList = Mapper.Map<IEnumerable<VehicleMakeViewModel>>(Vehicles.OrderBy(x => x.VMakeID)).ToPagedList(page ?? 1, PageSize);
                    break;
            }
            return VehiclesList;
        }

        public List<VehicleMake> AllVehicleMake()
        {
            List<VehicleMake> vehicleMakeList =  Db.VehicleMakes.ToList();
            return vehicleMakeList;
        }
    }
}
