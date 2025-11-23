using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Indexes;

namespace Data.Repositories
{
    //[AutoRegister]
    public class Repository<T> : IRepository<T> where T : IdObject
    {
        //protected static readonly ConcurrentDictionary<Guid, T> Store = new ConcurrentDictionary<Guid, T>();

        private readonly IDocumentSession _documentSession;

        
        public Repository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public void Save(T entity)
        {
            _documentSession.Store(entity);
            //_documentSession.SaveChanges(); // commit to RavenDB
        }

        public void Delete(T entity)
        {
            _documentSession.Delete(entity);
        }

        public T Get(Guid id)
        {
            return _documentSession.Load<T>(id);
        }

        protected void DeleteAll<TIndex>() where TIndex : AbstractIndexCreationTask<T>
        {
            _documentSession.Advanced.DocumentStore.DatabaseCommands.DeleteByIndex(typeof(TIndex).Name, new IndexQuery());
        }
        /*

        public void Save(T entity)
        {
            // Upsert semantics: add or replace
            Store[entity.Id] = entity;
        }

        public void Delete(T entity)
        {
            Store.TryRemove(entity.Id, out _);
        }

        public T Get(Guid id)
        {
            Store.TryGetValue(id, out var entity);
            return entity;
        }

        protected void DeleteAll()
        {
            Store.Clear();
        }
        */
    }
}