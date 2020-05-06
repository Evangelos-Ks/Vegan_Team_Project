﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vegan.Database;
using Vegan.Entities.Home;
using Vegan.Services;
using System.ComponentModel.DataAnnotations;

namespace Vegan.Web.Controllers.TestControllers
{
    public class KitchenController : Controller
    {
        //===================================== Fields =====================================================================
        private UnitOfWork unitOfWork = new UnitOfWork(new MyDatabase());


        //private GenericRepository<Kitchen> repository;

        //===================================== Constructor ================================================================
        //public KitchenController()
        //{
        //    repository = new GenericRepository<Kitchen>(unitOfWork);
        //}

        //===================================== Methods ====================================================================
        [HttpGet]
        public ActionResult Index()
        {
            return View(unitOfWork.Kitchens.GetAll());
        }

        [HttpGet]
        public ActionResult AddKitchen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddKitchen(Kitchen model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Kitchens.Add(model);
                    unitOfWork.Complete();
                    unitOfWork.Dispose();
                    return RedirectToAction("Index", "Kitchen");
                }
            }
            catch (Exception ex)
            {
                //TODO: We want to show an error message
                return View();
            }
            return View();
        }

        public ActionResult DetailsKitchen(int productId)
        {
            return View(unitOfWork.Kitchens.GetById(productId));
        }

        [HttpGet]
        public ActionResult EditKitchen(int productId)
        {
            return View(unitOfWork.Kitchens.GetById(productId));
        }

        [HttpPost]
        public ActionResult EditKitchen(Kitchen model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Kitchens.Edit(model);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return RedirectToAction("Index", "Kitchen");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteKitchen(int productId)
        {
            return View(unitOfWork.Kitchens.GetById(productId));
        }

        [HttpPost, ActionName("DeleteKitchen")]
        public ActionResult Delete(int productId)
        {

            var product = unitOfWork.Kitchens.GetById(productId);
            unitOfWork.Kitchens.Delete(product);
            unitOfWork.Complete();
            unitOfWork.Dispose();
            return RedirectToAction("Index", "Kitchen");
        }
    }
}