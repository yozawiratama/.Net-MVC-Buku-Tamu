using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BukuTamuTest.Models;
using Microsoft.EntityFrameworkCore;

namespace BukuTamuTest.Controllers;

public class TamuController : Controller
{
    private readonly ILogger<TamuController> _logger;

    private readonly ApplicationContext _ctx; //context, dbContext

    public TamuController(ILogger<TamuController> logger, ApplicationContext context)
    {
        _logger = logger;
        _ctx = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _ctx.Tamus.ToListAsync());
    }

    public IActionResult Tambah()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Tambah([Bind("Nama,Alamat")] Tamu tamu)
    {
        if (ModelState.IsValid)
        {
            tamu.TanggalWaktu = DateTime.Now;
            _ctx.Tamus.Add(tamu);
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(tamu);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tamu = _ctx.Tamus.Find(id);

        if (tamu == null)
        {
            return NotFound();
        }

        return View(tamu);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Nama,Alamat")] Tamu tamu)
    {
        if (id != tamu.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var found = _ctx.Tamus.Find(id);

            if (found == null) return NotFound();

            found.Nama = tamu.Nama;
            found.Alamat = tamu.Alamat;
            _ctx.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(tamu);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var tamu = _ctx.Tamus.Find(id);
        if (tamu != null)
        {
            _ctx.Tamus.Remove(tamu);
            _ctx.SaveChanges();
        }
        else return NotFound();

        return RedirectToAction(nameof(Index)); 
    }



}