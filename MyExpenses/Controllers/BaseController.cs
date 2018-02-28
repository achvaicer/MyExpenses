using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyExpenses.Models;
using MyExpenses.Repository;

namespace MyExpenses.Controllers
{
    public abstract class BaseController<TEntity, TRepository> : Controller
        where TEntity : BaseEntity, new()
        where TRepository : IRepository<TEntity>
    {
        protected TRepository _repository;
        protected string DefaultOrderBy = "Nome";
        protected int Rows = 20;



        protected virtual void PopulateViewBag(params object[] args)
        {
        }

        protected virtual string CheckArgs(object[] args, int index)
        {
            return args != null && args.Length > index && args[index] != null ? args[index].ToString() : null;
        }

        public virtual ActionResult Index()
        {
            ViewBag.TotalRows = _repository.Count();
            ViewBag.PageNumber = 1;
            ViewBag.RowsPerPage = Rows;
            ViewBag.OrderBy = DefaultOrderBy;
            return View(_repository.Paged(DefaultOrderBy, 1, Rows));
        }

        public virtual ActionResult Paged(string orderBy, int? page, int? rows)
        {
            ViewBag.TotalRows = _repository.Count();
            ViewBag.PageNumber = page ?? 1;
            ViewBag.RowsPerPage = rows ?? Rows;
            ViewBag.OrderBy = orderBy ?? DefaultOrderBy;
            return View("Index", _repository.Paged(ViewBag.OrderBy as string, (int)ViewBag.PageNumber, (int)ViewBag.RowsPerPage));
        }

        public virtual ActionResult Criar()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public virtual ActionResult Criar(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(entity);
                return RedirectToAction("Index");
            }
            PopulateViewBag();
            return View(entity);
        }

        public virtual ActionResult Editar(long id)
        {
            PopulateViewBag();
            return View(_repository.Single(id));
        }

        [HttpPost]
        public virtual ActionResult Editar(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(entity);
                return RedirectToAction("Index");
            }
            PopulateViewBag();
            return View(entity);
        }

        [HttpDelete]
        public virtual JsonResult Deletar(long id)
        {
            try
            {
                var entidade = _repository.Single(id);
                _repository.Delete(id);
                var t = entidade.GetType();
                var p = t.GetProperty("Nome");
                var n = p.GetValue(entidade, null) ?? "";
                return Json(new { Status = "Success", Entidade = entidade, Mensagem = string.Format("{0} {1} excluído/a com sucesso", t.Name, n) });
            }
            catch (Exception exception)
            {
                return Json(new { Status = "Error", Exception = exception.Message });
            }
        }
    }
}
