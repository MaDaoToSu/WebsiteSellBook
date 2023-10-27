﻿using Microsoft.AspNetCore.Mvc;
using SellBook.Models;
using SellBook.DataAccess;
using SellBook.DataAccess.Repository.IRepository;

namespace WebsiteSellBook.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
			return View(objCategoryList);
		}

		[HttpGet]
		public IActionResult CreateCategory()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Add(category);
				_unitOfWork.Save();
				TempData["Success"] = "Create new category successful !!!";
				TempData["Title"] = "Create category";
				return RedirectToAction("Index");
			}
			else
			{
				return View(category);
			}

		}

		[HttpGet]
		public IActionResult EditCategory(int? id)
		{
			var exitId = _unitOfWork.Category.Get(item => item.Category_ID == id);
			return View(exitId);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditCategory(Category category)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Update(category);
				_unitOfWork.Save();
				TempData["Success"] = "Update category successful !!!";
				TempData["Title"] = "Update category";
				return RedirectToAction("Index");
			}

			return View(category);

		}





		public IActionResult DeleteCategory(int id)
		{
			var exitId = _unitOfWork.Category.Get(item => item.Category_ID == id);
			if (exitId != null)
			{
				_unitOfWork.Category.Remove(exitId);
				_unitOfWork.Save();
				TempData["Title"] = "Remove category";
				TempData["Success"] = "Remove category successful !!!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");

		}



	}
}