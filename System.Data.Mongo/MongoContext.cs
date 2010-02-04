﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace System.Data.Mongo
{
    public class MongoContext
    {
        /// <summary>
        /// This indicates if the context should load properties 
        /// that are not part of a given class definition into a 
        /// special flyweight lookup. 
        /// 
        /// Disabled by default.
        /// </summary>
        /// <remarks>
        /// This is useful when the class definition you want to use doesn't support a particular property, but the database should 
        /// still maintain it, or you do not want to squash it on save.
        /// 
        /// Enabling this will cause additinal overhead when loading/saving, as well as more memory consumption during the lifetime of the object.
        /// </remarks>
        public bool EnableExpandoProperties
        {
            get;
            private set;
        }

        /// <summary>
        /// Number of seconds to wait for a response from the server before throwing a timeout exception.
        /// Defaults to 30.
        /// </summary>
        public int QueryTimeout
        {
            get;
            set;
        }
        /// <summary>
        /// The ip/domain name of the server.
        /// </summary>
        protected String _serverName = "127.0.0.1";
        /// <summary>
        /// The port on which the server is accessible.
        /// </summary>
        protected int _serverPort = 27017;

        protected IPEndPoint _endPoint;

        /// <summary>
        /// Specify the host and the port to connect to for the mongo db.
        /// </summary>
        /// <param name="server">The server IP or hostname (127.0.0.1 is the default)</param>
        /// <param name="port">The port on which mongo is running (27017 is the default)</param>
        /// <param name="enableExpandoProps">Should requests to this database push/pull props from the DB that are not part of the specified object?</param>
        public MongoContext(String server, int port, bool enableExpandoProps)
        {
            this.QueryTimeout = 30;
            this._serverName = server;
            this._serverPort = port;
            this.EnableExpandoProperties = enableExpandoProps;
        }

        

        
        
        /// <summary>
        /// Creates a context that will connect to 127.0.0.1:27017 (MongoDB on the default port).
        /// </summary>
        /// <remarks>
        /// This also disabled Expando props for documents.
        /// </remarks>
        public MongoContext()
            : this("127.0.0.1", 27017, false)
        {

        }

        /// <summary>
        /// Will provide an object reference to a DB within the current context.
        /// </summary>
        /// <remarks>
        /// I would recommend adding extension methods, or subclassing MongoContext 
        /// to provide strongly-typed members for each database in your context, this
        /// will removed strings except for a localized places - which can should 
        /// reduce typo problems..
        /// </remarks>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public MongoDatabase GetDatabase(String dbName)
        {
            var retval = new MongoDatabase(dbName, this);
            return retval;
        }

        /// <summary>
        /// Constructs a socket to the server.
        /// </summary>
        /// <returns></returns>
        internal TcpClient ServerConnection()
        {
            return new TcpClient(this._serverName, this._serverPort);

        }

        /// <summary>
        /// Returns a list of databases that already exist on this context.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<String> GetAllDatabases()
        {


            yield break;
        }


    }
}