﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopLibrary;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Products;
using TeamWebShop.Models.ViewModels.Products;

namespace TeamWebShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly IMapper mapper;

        public ProductsController(ShopContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(await shopContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            CreateProductVM viewModel = new CreateProductVM
            {
                Brands = new SelectList(_context.Brands, "Id", "BrandName"),
                Categories = new SelectList(_context.Categories, "Id", "CategoryName")
            };

            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM viewModel)
        {
            if (ModelState.IsValid)
            {
                Product product = mapper.Map<Product>(viewModel.ProductDTO);
                _context.Add(product);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Brands = new SelectList(_context.Brands, "Id", "BrandName");
            viewModel.Categories = new SelectList(_context.Categories, "Id", "CategoryName");

            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            CreateProductVM viewModel = new CreateProductVM
            {
                ProductDTO = mapper.Map<ProductDTO>(product),
                Brands = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId),
                Categories = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId)
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateProductVM viewModel)
        {
            if (id != viewModel.ProductDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product product = mapper.Map<Product>(viewModel.ProductDTO);
                    _context.Update(product);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.ProductDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            viewModel.Brands = new SelectList(_context.Brands, "Id", "BrandName", viewModel.ProductDTO.BrandId);
            viewModel.Categories = new SelectList(_context.Categories, "Id", "CategoryName", viewModel.ProductDTO.CategoryId);

            return View(viewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
