using System;
using System.Net.Sockets;

namespace Manager.Aplication
{ 
    public class WorkerMetadata
        {
            private string host;
            private int port;


            public WorkerMetadata(string host, int port)
            {
                this.Host = host;
                this.Port = port;
            }

            public string Host { get; }
            public int    Port { get; }
            
            public bool IsAlive()
            {
                try
                {
                    using var client = new TcpClient(this.Host, this.Port);
                    return true;
                }
                catch (SocketException exception)
                {
                    return false;
                }
            }
        
            protected bool Equals(WorkerMetadata other) =>
                this.Host == other.Host && this.Port == other.Port;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return this.Equals((WorkerMetadata) obj);
            }

            public override int GetHashCode() =>
                HashCode.Combine(this.Host, this.Port);
        }
    
}