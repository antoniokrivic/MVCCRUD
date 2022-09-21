﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Service.DAL;
using Project.Service.Models;
using PagedList;
using Project.Service.ViewModels;
using AutoMapper;

namespace Project.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        private VehicleService vehicleService;

        public VehicleModelController()
        {
            vehicleService = VehicleService.GetInstance();
        }

        // GET: VehicleModel
        public ActionResult Index(string Condition_sort, string currentFilter, string Condition_search, int? page)
        {
            ViewBag.CurrentSort = Condition_sort;
            ViewBag.NameSortParm = Condition_sort == "Name" ? "Name_desc" : "Name";
            ViewBag.ModelSortParm = Condition_sort == "Model" ? "Model_desc" : "Model";
            ViewBag.AbrvSortParm = Condition_sort == "Abrv" ? "Abrv_desc" : "Abrv";
            ViewBag.CurrentFilter = Condition_search;
            var vehicles = vehicleService.SearchSortVehicleModel(Condition_sort, page, Condition_search, currentFilter);
            
            return View(vehicles);
        }

        // GET: VehicleModel/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // GET: VehicleModel/Create
        public ActionResult Create()
        {
            ViewBag.VMakeID = vehicleService.AllVehicleMake();
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VModelID,VMakeID,Name,Model,Abrv")]VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                vehicleService.CreateVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }

            ViewBag.VMakeID = vehicleService.AllVehicleMake();
            return View(vehicleModel);
        }

        // GET: VehicleModel/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.VMakeID = vehicleService.AllVehicleMake();
            return View(vehicleModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VModelID,VMakeID,Name,Model,Abrv")] VehicleModelViewModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                vehicleService.UpdateVehicleModel(vehicleModel);
                return RedirectToAction("Index");
            }
            ViewBag.VMakeID = vehicleService.AllVehicleMake();
            return View(vehicleModel);
        }

        // GET: VehicleModel/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel vehicleModel = vehicleService.FindVehicleModel(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            vehicleService.DeleteVehicleModel(id);
            return RedirectToAction("Index");
        }
    }
}
