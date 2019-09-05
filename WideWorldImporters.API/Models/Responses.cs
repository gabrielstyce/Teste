using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WideWorldImporters.API.Models
{
    public interface IResponse
    {
        string Message { get; set; }
        bool DidError { get; set; }
        string ErrorMessage { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }
        double PageCount { get; }
    }

    public class Response : IResponse
    {
        public string Message { get; set; }
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public TModel Model { get; set; }
        public string Message { get; set; }
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public IEnumerable<TModel> Model { get; set; }
        public string Message { get; set; }
        public bool DidError { get; set; }
        public string ErrorMessage { get; set; }
    }
}
