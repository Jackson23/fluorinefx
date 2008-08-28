/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using FluorineFx.HttpCompress;

namespace FluorineFx.Configuration
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	[XmlType(TypeName="settings")]
	public sealed class FluorineSettings
	{
		private ClassMappingCollection _classMappings;
		private ServiceCollection _services;
		private NullableTypeCollection _nullables;
		private LoginCommandCollection _loginCommandCollection;
		private CacheCollection _cache;
		private ImportNamespaceCollection _importedNamespaces;
		private HttpCompressSettings _httpCompressSettings;
		private bool _wsdlGenerateProxyClasses;
		private string _wsdlProxyNamespace;
		private bool _acceptNullValueTypes;
		private RemotingServiceAttributeConstraint _remotingServiceAttributeConstraint;
		private TimezoneCompensation _timezoneCompensation;
		private RtmpServerSettings _rtmpServerSettings;
		private OptimizerSettings _optimizerSettings;
		private SwxSettings _swxSettings;
        private StreamableFileFactorySettings _streamableFileFactorySettings;
        private BWControlServiceSettings _bwControlServiceSettings;
        private SchedulingServiceSettings _schedulingServiceSettings;
        private PlaylistSubscriberStreamSettings _playlistSubscriberStreamSettings;
        private JSonSettings _jsonSettings;
        private RuntimeSettings _runtimeSettings;

        /// <summary>
        /// Initializes a new instance of the FluorineSettings class.
        /// </summary>
		public FluorineSettings()
		{
			_timezoneCompensation = TimezoneCompensation.None;
			_remotingServiceAttributeConstraint = RemotingServiceAttributeConstraint.Access;
			_acceptNullValueTypes = false;
			_wsdlProxyNamespace = "FluorineFx.Proxy";
			_wsdlGenerateProxyClasses = true;
			_rtmpServerSettings = new RtmpServerSettings();
			//_optimizerSettings = new OptimizerSettings();
			_swxSettings = new SwxSettings();
            _runtimeSettings = new RuntimeSettings();
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("classMappings")]
		[XmlArrayItem("classMapping",typeof(ClassMapping))]
		public ClassMappingCollection ClassMappings
		{
			get
			{
				if (_classMappings == null)
					_classMappings = new ClassMappingCollection();
				return _classMappings;
			}
			//set{ _classMappings = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("services")]
		[XmlArrayItem("service",typeof(ServiceConfiguration))]
		public ServiceCollection Services
		{
			get
			{
				if (_services == null)
					_services = new ServiceCollection();
				return _services;
			}
			//set{ _services = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("nullable")]
		[XmlArrayItem("type",typeof(NullableType))]
		public NullableTypeCollection Nullables
		{
			get
			{
				if (_nullables == null)
					_nullables = new NullableTypeCollection();
				return _nullables;
			}
			//set{ _nullables = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("cache")]
		[XmlArrayItem("cachedService",typeof(CachedService))]
		public CacheCollection Cache
		{
			get
			{
				if (_cache == null)
					_cache = new CacheCollection();
				return _cache;
			}
			//set{ _cache = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("security")]
		[XmlArrayItem("login-command",typeof(LoginCommandSettings))]
		public LoginCommandCollection LoginCommands
		{
			get
			{ 
				if (_loginCommandCollection == null)
					_loginCommandCollection = new LoginCommandCollection();
				return _loginCommandCollection;
			}
			//set{ _loginCommandCollection = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("importNamespaces")]
		[XmlArrayItem("add",typeof(ImportNamespace))]
		public ImportNamespaceCollection ImportNamespaces
		{
			get
			{
				if (_importedNamespaces == null)
					_importedNamespaces = new ImportNamespaceCollection();
				return _importedNamespaces;
			}
			//set{ _importedNamespaces = value; }
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "streamableFileFactory")]
        public StreamableFileFactorySettings StreamableFileFactory
        {
            get
            {
                if (_streamableFileFactorySettings == null)
                    _streamableFileFactorySettings = new StreamableFileFactorySettings();
                return _streamableFileFactorySettings;
            }
            set { _streamableFileFactorySettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "bwControlService")]
        public BWControlServiceSettings BWControlService
        {
            get
            {
                if (_bwControlServiceSettings == null)
                    _bwControlServiceSettings = new BWControlServiceSettings();
                return _bwControlServiceSettings;
            }
            set { _bwControlServiceSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "schedulingService")]
        public SchedulingServiceSettings SchedulingService
        {
            get
            {
                if (_schedulingServiceSettings == null)
                    _schedulingServiceSettings = new SchedulingServiceSettings();
                return _schedulingServiceSettings;
            }
            set { _schedulingServiceSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "playlistSubscriberStream")]
        public PlaylistSubscriberStreamSettings PlaylistSubscriberStream
        {
            get
            {
                if (_playlistSubscriberStreamSettings == null)
                    _playlistSubscriberStreamSettings = new PlaylistSubscriberStreamSettings();
                return _playlistSubscriberStreamSettings;
            }
            set { _playlistSubscriberStreamSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "httpCompress")]
		public HttpCompressSettings HttpCompressSettings
		{
			get{ return _httpCompressSettings; }
			set{ _httpCompressSettings = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "wsdlGenerateProxyClasses")]
		public bool WsdlGenerateProxyClasses
		{
			get{ return _wsdlGenerateProxyClasses; }
			set{ _wsdlGenerateProxyClasses = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "wsdlProxyNamespace")]
		public string WsdlProxyNamespace
		{
			get{ return _wsdlProxyNamespace; }
			set{ _wsdlProxyNamespace = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "timezoneCompensation")]
		public TimezoneCompensation TimezoneCompensation
		{
			get{ return _timezoneCompensation; }
			set{ _timezoneCompensation = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "acceptNullValueTypes")]
		public bool AcceptNullValueTypes
		{
			get{ return _acceptNullValueTypes; }
			set{ _acceptNullValueTypes = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "remotingServiceAttribute")]
		public RemotingServiceAttributeConstraint RemotingServiceAttribute
		{
			get{ return _remotingServiceAttributeConstraint; }
			set{ _remotingServiceAttributeConstraint = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "rtmpServer")]
		public RtmpServerSettings RtmpServer
		{
			get{ return _rtmpServerSettings; }
			set{ _rtmpServerSettings = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "optimizer")]
		public OptimizerSettings Optimizer
		{
			get{ return _optimizerSettings; }
			set{ _optimizerSettings = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "swx")]
		public SwxSettings SwxSettings
		{
			get{ return _swxSettings; }
			set{ _swxSettings = value; }
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "json")]
        public JSonSettings JSonSettings
        {
            get
            {
                if (_jsonSettings == null)
                    _jsonSettings = new JSonSettings();
                return _jsonSettings;
            }
            set { _jsonSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "runtime")]
        public RuntimeSettings Runtime
        {
            get { return _runtimeSettings; }
            set { _runtimeSettings = value; }
        }

	}

	#region LoginCommandCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class LoginCommandCollection : CollectionBase
	{
		Hashtable _loginCommands;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public LoginCommandCollection()
		{
			_loginCommands = new Hashtable();
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( LoginCommandSettings value )  
		{
			_loginCommands[value.Server] = value.Type;
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( LoginCommandSettings value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, LoginCommandSettings value )  
		{
			_loginCommands[value.Server] = value.Type;
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( LoginCommandSettings value )  
		{
			_loginCommands.Remove(value.Server);
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( LoginCommandSettings value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public LoginCommandSettings this[ int index ]  
		{
			get  
			{
				return List[index] as LoginCommandSettings;
			}
			set  
			{
				List[index] = value;
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
		public string GetLoginCommand(string server)
		{
			return _loginCommands[server] as string;
		}
	}

	#endregion LoginCommandCollection

	#region LoginCommandSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    [XmlType(TypeName = "login-command")]
	public sealed class LoginCommandSettings
	{
		private string _type;
		private string _server;

        public const string FluorineLoginCommand = "asp.net";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public LoginCommandSettings()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "class")]
		public string Type
		{
			get{return _type;}
			set{_type = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "server")]
		public string Server
		{
			get{return _server;}
			set{_server = value;}
		}
	}

	#endregion LoginCommandSettings

	#region ClassMappingCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class ClassMappingCollection : CollectionBase
	{
		private Hashtable _typeToCustomClass;
		private Hashtable _customClassToType;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ClassMappingCollection()
		{
			_typeToCustomClass = new Hashtable();
			_customClassToType = new Hashtable();

			Add("FluorineFx.AMF3.ArrayCollection", "flex.messaging.io.ArrayCollection");
			Add("FluorineFx.AMF3.ByteArray", "flex.messaging.io.ByteArray");
			Add("FluorineFx.AMF3.ObjectProxy", "flex.messaging.io.ObjectProxy");

			//FDS
			Add("FluorineFx.Messaging.Messages.CommandMessage", "flex.messaging.messages.CommandMessage");
			Add("FluorineFx.Messaging.Messages.RemotingMessage", "flex.messaging.messages.RemotingMessage");
			Add("FluorineFx.Messaging.Messages.AsyncMessage", "flex.messaging.messages.AsyncMessage");
			Add("FluorineFx.Messaging.Messages.AcknowledgeMessage", "flex.messaging.messages.AcknowledgeMessage");
			Add("FluorineFx.Data.Messages.DataMessage", "flex.data.messages.DataMessage");
			Add("FluorineFx.Data.Messages.PagedMessage", "flex.data.messages.PagedMessage");
			Add("FluorineFx.Data.Messages.UpdateCollectionMessage", "flex.data.messages.UpdateCollectionMessage");
			Add("FluorineFx.Data.Messages.SequencedMessage", "flex.data.messages.SequencedMessage");
            Add("FluorineFx.Data.Messages.DataErrorMessage", "flex.data.messages.DataErrorMessage");
            Add("FluorineFx.Messaging.Messages.ErrorMessage", "flex.messaging.messages.ErrorMessage");
			Add("FluorineFx.Messaging.Messages.RemotingMessage", "flex.messaging.messages.RemotingMessage");
			Add("FluorineFx.Messaging.Messages.RPCMessage", "flex.messaging.messages.RPCMessage");			

			Add("FluorineFx.Data.UpdateCollectionRange", "flex.data.UpdateCollectionRange");			
			
			Add("FluorineFx.Messaging.Services.RemotingService", "flex.messaging.services.RemotingService");
			Add("FluorineFx.Messaging.Services.MessageService", "flex.messaging.services.MessageService");
			Add("FluorineFx.Data.DataService", "flex.data.DataService");
            Add("FluorineFx.Messaging.Endpoints.RtmpEndpoint", "flex.messaging.endpoints.RTMPEndpoint");
            Add("FluorineFx.Messaging.Endpoints.AMFEndpoint", "flex.messaging.endpoints.AMFEndpoint");

			Add("FluorineFx.Messaging.Services.Remoting.DotNetAdapter", "flex.messaging.services.remoting.adapters.JavaAdapter");
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="customClass"></param>
		public void Add(string type, string customClass)
		{
			ClassMapping classMapping = new ClassMapping();
			classMapping.Type = type;
			classMapping.CustomClass = customClass;
			this.Add(classMapping);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( ClassMapping value )  
		{
			_typeToCustomClass[value.Type] = value.CustomClass;
			_customClassToType[value.CustomClass] = value.Type;

			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( ClassMapping value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, ClassMapping value )  
		{
			_typeToCustomClass[value.Type] = value.CustomClass;
			_customClassToType[value.CustomClass] = value.Type;

			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( ClassMapping value )  
		{
			_typeToCustomClass.Remove(value.Type);
			_customClassToType.Remove(value.CustomClass);

			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( ClassMapping value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public ClassMapping this[ int index ]  
		{
			get  
			{
				return List[index] as ClassMapping;
			}
			set  
			{
				List[index] = value;
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public string GetCustomClass(string type)
		{
			if( _typeToCustomClass.Contains( type ) )
				return _typeToCustomClass[type] as string;
			else
				return type;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="customClass"></param>
        /// <returns></returns>
		public string GetType(string customClass)
		{
			if( customClass == null )
				return null;
			if( _customClassToType.Contains(customClass) )
				return _customClassToType[customClass] as string;
			else
				return customClass;
		}
	}

	#endregion ClassMappingCollection

	#region ClassMapping

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    [XmlType(TypeName = "classMapping")]
	public sealed class ClassMapping
	{
		private string _type;
		private string _customClass;
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ClassMapping()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "type")]
		public string Type
		{
			get{return _type;}
			set{_type = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "customClass")]
		public string CustomClass
		{
			get{return _customClass;}
			set{_customClass = value;}
		}
	}

	#endregion ClassMapping

	#region ServiceCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class ServiceCollection : CollectionBase
	{
		private Hashtable _serviceNames;
		private Hashtable _serviceLocations;
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ServiceCollection()
		{
			_serviceNames = new Hashtable(5);
			_serviceLocations = new Hashtable(5);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( ServiceConfiguration value )  
		{
			_serviceNames[value.Name] = value;
			_serviceLocations[value.ServiceLocation] = value;
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( ServiceConfiguration value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, ServiceConfiguration value )  
		{
			_serviceNames[value.Name] = value;
			_serviceLocations[value.ServiceLocation] = value;
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( ServiceConfiguration value )
		{
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( ServiceConfiguration value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
		public bool Contains(string serviceName)
		{
			return _serviceNames.Contains(serviceName);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public ServiceConfiguration this[ int index ]  
		{
			get  
			{
				return List[index] as ServiceConfiguration;
			}
			set  
			{
				List[index] = value;
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
		public string GetServiceLocation(string serviceName)
		{
			if( _serviceNames.Contains(serviceName) )
				return (_serviceNames[serviceName] as ServiceConfiguration).ServiceLocation;
			else
				return serviceName;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="serviceLocation"></param>
        /// <returns></returns>
		public string GetServiceName(string serviceLocation)
		{
			if( _serviceLocations.Contains(serviceLocation) )
				return (_serviceLocations[serviceLocation] as ServiceConfiguration).Name;
			else
				return serviceLocation;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
		public string GetMethod(string serviceName, string name)
		{
			ServiceConfiguration serviceConfiguration = _serviceNames[serviceName] as ServiceConfiguration;
			if( serviceConfiguration != null )
				return serviceConfiguration.Methods.GetMethod(name);
			return name;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="serviceLocation"></param>
        /// <param name="method"></param>
        /// <returns></returns>
		public string GetMethodName(string serviceLocation, string method)
		{
			ServiceConfiguration serviceConfiguration = _serviceLocations[serviceLocation] as ServiceConfiguration;
			if( serviceConfiguration != null )
				return serviceConfiguration.Methods.GetMethodName(method);
			return method;
		}
	}

	#endregion ServiceCollection

	#region ServiceConfiguration

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    [XmlType(TypeName = "service")]
	public sealed class ServiceConfiguration
	{
		private string _name;
		private string _serviceLocation;
		private RemoteMethodCollection _remoteMethodCollection;
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ServiceConfiguration()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "name")]
		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "service-location")]
		public string ServiceLocation
		{
			get{return _serviceLocation;}
			set{_serviceLocation = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("methods")]
		[XmlArrayItem("remote-method",typeof(RemoteMethod))]
		public RemoteMethodCollection Methods
		{
			get
			{
				if (_remoteMethodCollection == null)
					_remoteMethodCollection = new RemoteMethodCollection();
				return _remoteMethodCollection;
			}
			//set{ _remoteMethodCollection = value; }
		}
	}
	
	#endregion ServiceConfiguration

	#region RemoteMethodCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RemoteMethodCollection : CollectionBase
	{
		Hashtable	_methods;
		Hashtable	_methodsNames;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public RemoteMethodCollection()
		{
			_methods = new Hashtable(3);
			_methodsNames = new Hashtable(3);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( RemoteMethod value )  
		{
			_methods[value.Name] = value.Method;
			_methodsNames[value.Method] = value.Name;
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( RemoteMethod value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, RemoteMethod value )  
		{
			_methods[value.Name] = value.Method;
			_methodsNames[value.Method] = value.Name;

			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( RemoteMethod value )  
		{
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( RemoteMethod value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public RemoteMethod this[ int index ]  
		{
			get  
			{
				return List[index] as RemoteMethod;
			}
			set  
			{
				List[index] = value;
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
		public string GetMethod(string name)
		{
			if( _methods.Contains(name) )
				return _methods[name] as string;
			return name;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
		public string GetMethodName(string method)
		{
			if( _methodsNames.Contains(method) )
				return _methodsNames[method] as string;
			return method;
		}
	}

	#endregion RemoteMethodCollection

	#region RemoteMethod

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RemoteMethod
	{
		private string _name;
		private string _method;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "name")]
		public string Name
		{
			get{return _name;}
			set{_name = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(DataType = "string", ElementName = "method")]
		public string Method
		{
			get{return _method;}
			set{_method = value;}
		}
	}

	#endregion RemoteMethod

	#region NullableTypeCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class NullableTypeCollection : CollectionBase
	{
		Hashtable _nullableDictionary;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public NullableTypeCollection()
		{
			_nullableDictionary = new Hashtable(5);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( NullableType value )  
		{
			_nullableDictionary[value.TypeName] = value;
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( NullableType value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, NullableType value )  
		{
			_nullableDictionary[value.TypeName] = value;
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( NullableType value )  
		{
			_nullableDictionary.Remove(value.TypeName);
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( NullableType value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public bool ContainsKey( Type type )  
		{
			return _nullableDictionary.ContainsKey(type.FullName);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public NullableType this[ int index ]  
		{
			get  
			{
				return List[index] as NullableType;
			}
			set  
			{
				List[index] = value;
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public object this[ Type type ]  
		{
			get  
			{
				if( _nullableDictionary.ContainsKey(type.FullName))
					return (_nullableDictionary[type.FullName] as NullableType).NullValue;
				return null;
			}
		}
	}

	#endregion NullableTypeCollection

	#region NullableType

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    [XmlType(TypeName = "type")]
	public sealed class NullableType
	{
		private string _typeName;
		private string _value;
		private object _nullValue;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "name")]
		public string TypeName
		{
			get{return _typeName;}
			set
			{
				_typeName = value;
				Init();
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "value")]
		public string Value
		{
			get{return _value;}
			set
			{
				_value = value;
				Init();
			}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlIgnore]
		public object NullValue
		{
			get{return _nullValue;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		private void Init()
		{
			if( _typeName == null || _value == null )
				return;

			Type type = Type.GetType(_typeName);
			// Check if this is a static field of the type
			FieldInfo fi = type.GetField(_value, BindingFlags.Public | BindingFlags.Static);
			if( fi != null )
				_nullValue = fi.GetValue(null);
			else
				_nullValue = System.Convert.ChangeType(_value, type);
		}
	}

	#endregion NullableType

	#region CacheCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class CacheCollection : CollectionBase
	{
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public CacheCollection()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( CachedService value )  
		{
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( CachedService value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, CachedService value )  
		{
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( CachedService value )  
		{
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( CachedService value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public CachedService this[ int index ]  
		{
			get  
			{
				return List[index] as CachedService;
			}
			set  
			{
				List[index] = value;
			}
		}
	}

	#endregion CacheCollection

	#region CachedService

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    [XmlType(TypeName = "cachedService")]
	public sealed class CachedService
	{
		private int _timeout;
		private bool _slidingExpiration;
		private string _type;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public CachedService()
		{
			_timeout = 30;
			_slidingExpiration = false;
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "timeout")]
		public int Timeout
		{
			get{return _timeout;}
			set{_timeout = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "boolean", AttributeName = "slidingExpiration")]
		public bool SlidingExpiration
		{
			get{return _slidingExpiration;}
			set{_slidingExpiration = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "type")]
		public string Type
		{
			get{return _type;}
			set{_type = value;}
		}
	}

	#endregion CachedService

	#region ImportNamespaceCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class ImportNamespaceCollection : CollectionBase
	{
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ImportNamespaceCollection()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( ImportNamespace value )  
		{
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( ImportNamespace value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, ImportNamespace value )  
		{
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( ImportNamespace value )  
		{
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( ImportNamespace value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public ImportNamespace this[ int index ]  
		{
			get  
			{
				return List[index] as ImportNamespace;
			}
			set  
			{
				List[index] = value;
			}
		}
	}

	#endregion ImportNamespaceCollection

	#region ImportNamespace

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class ImportNamespace
	{
		private string _namespace;
		private string _assembly;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ImportNamespace()
		{
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "namespace")]
		public string Namespace
		{
			get{return _namespace;}
			set{_namespace = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "assembly")]
		public string Assembly
		{
			get{return _assembly;}
			set{_assembly = value;}
		}
	}

	#endregion ImportNamespace

    #region StreamableFileFactorySettings

    /// <summary>
    /// StreamableFileFactory settings.
    /// </summary>
    public class StreamableFileFactorySettings
    {
        private string _type;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public StreamableFileFactorySettings()
        {
            _type = typeof(FluorineFx.Messaging.Rtmp.IO.StreamableFileFactory).FullName;
        }

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }

    #endregion StreamableFileFactorySettings

    #region BWControlServiceSettings

    /// <summary>
    /// BWControlServiceSettings settings.
    /// </summary>
    public class BWControlServiceSettings
    {
        private string _type;
        private int _interval;
        private int _defaultCapacity;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public BWControlServiceSettings()
        {
            _type = typeof(FluorineFx.Messaging.Rtmp.Stream.DummyBWControlService).FullName;
            _interval = 100;
            _defaultCapacity = 104857600;
        }

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "interval")]
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "defaultCapacity")]
        public int DefaultCapacity
        {
            get { return _defaultCapacity; }
            set { _defaultCapacity = value; }
        }
    }

    #endregion BWControlServiceSettings

    #region SchedulingServiceSettings

    /// <summary>
    /// SchedulingServiceSettings settings.
    /// </summary>
    public class SchedulingServiceSettings
    {
        private string _type;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public SchedulingServiceSettings()
        {
            _type = typeof(FluorineFx.Scheduling.SchedulingService).FullName;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }

    #endregion SchedulingServiceSettings

    #region PlaylistSubscriberStreamSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class PlaylistSubscriberStreamSettings
    {
        private int _underrunTrigger;
        private int _bufferCheckInterval;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public PlaylistSubscriberStreamSettings()
        {
            _underrunTrigger = 5000;
            _bufferCheckInterval = 60000;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "underrunTrigger")]
        public int UnderrunTrigger
        {
            get { return _underrunTrigger; }
            set { _underrunTrigger = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "bufferCheckInterval")]
        public int BufferCheckInterval
        {
            get { return _bufferCheckInterval; }
            set { _bufferCheckInterval = value; }
        }
    }

    #endregion PlaylistSubscriberStreamSettings

    #region HttpCompressSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class HttpCompressSettings
	{
		private HandleRequest _handleRequest;
		private Algorithms _preferredAlgorithm;
		private CompressionLevels _compressionLevel;
		private MimeTypeEntryCollection _excludedTypes;
		private PathEntryCollection _excludedPaths;
		int _threshold;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public HttpCompressSettings()
		{
			_handleRequest = HandleRequest.None;
			_preferredAlgorithm = Algorithms.Default;
			_compressionLevel = CompressionLevels.Default;
			_excludedTypes = new MimeTypeEntryCollection();
			_excludedPaths = new PathEntryCollection();
			_threshold = 20480;
		}

		/// <summary>
		/// The default settings.  Deflate + normal.
		/// </summary>
		public static HttpCompressSettings Default 
		{
			get { return new HttpCompressSettings(); }
		}

		/// <summary>
		/// Request type to handle.
		/// </summary>
		[XmlAttribute(AttributeName="handleRequest")]
		public HandleRequest HandleRequest
		{
			get { return _handleRequest; }
			set { _handleRequest = value; }
		}

		/// <summary>
		/// The preferred algorithm to use for compression.
		/// </summary>
		[XmlAttribute(AttributeName="preferredAlgorithm")]
		public Algorithms PreferredAlgorithm
		{
			get { return _preferredAlgorithm; }
			set { _preferredAlgorithm = value; }
		}

		/// <summary>
		/// Compress responses larger then threshold bytes.
		/// </summary>
		[XmlElement(ElementName="threshold")]
		public int Threshold
		{
			get{ return _threshold; }
			set{ _threshold = value; }
		}

		/// <summary>
		/// The preferred compression level.
		/// </summary>
		[XmlAttribute(AttributeName="compressionLevel")]
		public CompressionLevels CompressionLevel 
		{
			get { return _compressionLevel; }
			set { _compressionLevel = value; }
		}

		[XmlArray("excludedMimeTypes")]
		[XmlArrayItem("add",typeof(MimeTypeEntry))]
		//[XmlArrayItem("remove",typeof(MimeTypeEntry))]
		public MimeTypeEntryCollection ExcludedMimeTypes 
		{
			get 
			{ 
				if( _excludedTypes == null )
					_excludedTypes = new MimeTypeEntryCollection();
				return _excludedTypes; 
			}
			//set { _excludedTypes = value; }
		}

		[XmlArray("excludedPaths")]
		[XmlArrayItem("add",typeof(PathEntry))]
		//[XmlArrayItem("remove",typeof(PathEntry))]
		public PathEntryCollection ExcludedPaths 
		{
			get 
			{ 
				if( _excludedPaths == null )
					_excludedPaths = new PathEntryCollection();
				return _excludedPaths; 
			}
			//set { _excludedPaths = value; }
		}

		/// <summary>
		/// Checks a given mime type to determine if it has been excluded from compression
		/// </summary>
		/// <param name="mimetype">The MimeType to check.  Can include wildcards like image/* or */xml.</param>
		/// <returns>true if the mime type passed in is excluded from compression, false otherwise</returns>
		public bool IsExcludedMimeType(string mimetype) 
		{
			return _excludedTypes.Contains(mimetype.ToLower());
		}

		/// <summary>
		/// Looks for a given path in the list of paths excluded from compression.
		/// </summary>
		/// <param name="relUrl">the relative url to check</param>
		/// <returns>true if excluded, false if not</returns>
		public bool IsExcludedPath(string relUrl) 
		{
			return _excludedPaths.Contains(relUrl.ToLower());
		}
	}

	#endregion HttpCompressSettings

	#region MimeTypeEntryCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class MimeTypeEntryCollection : CollectionBase
	{
		StringCollection _excludedTypes;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public MimeTypeEntryCollection()
		{
			_excludedTypes = new StringCollection();
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( MimeTypeEntry value )  
		{
			_excludedTypes.Add(value.Type);
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( MimeTypeEntry value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, MimeTypeEntry value )  
		{
			_excludedTypes.Add(value.Type);
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( MimeTypeEntry value )  
		{
			_excludedTypes.Remove(value.Type);
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( MimeTypeEntry value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public bool Contains( string type )  
		{
			return _excludedTypes.Contains(type);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public MimeTypeEntry this[ int index ]  
		{
			get  
			{
				return List[index] as MimeTypeEntry;
			}
			set  
			{
				List[index] = value;
			}
		}
	}

	#endregion MimeTypeEntryCollection

	#region PathEntryCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class PathEntryCollection : CollectionBase
	{
		StringCollection _excludedPaths;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public PathEntryCollection()
		{
			_excludedPaths = new StringCollection();
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int Add( PathEntry value )  
		{
			_excludedPaths.Add(value.Path);
			return List.Add(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public int IndexOf( PathEntry value )  
		{
			return List.IndexOf(value) ;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert( int index, PathEntry value )  
		{
			_excludedPaths.Add(value.Path);
			List.Insert(index, value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
		public void Remove( PathEntry value )  
		{
			_excludedPaths.Remove(value.Path);
			List.Remove(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains( PathEntry value )  
		{
			return List.Contains(value);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
		public bool Contains( string path )  
		{
			return _excludedPaths.Contains(path);
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
		public PathEntry this[ int index ]  
		{
			get  
			{
				return List[index] as PathEntry;
			}
			set  
			{
				List[index] = value;
			}
		}
	}

	#endregion PathEntryCollection

	#region MimeTypeEntry

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class MimeTypeEntry
	{
		private string _type;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(AttributeName = "type")]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}
	}

	#endregion MimeTypeEntry

	#region PathEntry

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class PathEntry
	{
		private string _path;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(AttributeName = "path")]
		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}
	}

	#endregion PathEntry

	#region RtmpServerSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RtmpServerSettings
	{
		private ThreadPoolSettings _threadPoolSettings;
        private RtmpConnectionSettings _rtmpConnectionSettings;
        private RtmptConnectionSettings _rtmptConnectionSettings;
        private RtmpTransportSettings _rtmpTransportSettings;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public RtmpServerSettings()
		{
			_threadPoolSettings = new ThreadPoolSettings();
            _rtmpConnectionSettings = new RtmpConnectionSettings();
            _rtmptConnectionSettings = new RtmptConnectionSettings();
            _rtmpTransportSettings = new RtmpTransportSettings();
		}

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "threadpool")]
		public ThreadPoolSettings ThreadPoolSettings
		{
			get{return _threadPoolSettings;}
			set{_threadPoolSettings = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "rtmpConnection")]
        public RtmpConnectionSettings RtmpConnectionSettings
        {
            get { return _rtmpConnectionSettings; }
            set { _rtmpConnectionSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "rtmptConnection")]
        public RtmptConnectionSettings RtmptConnectionSettings
        {
            get { return _rtmptConnectionSettings; }
            set { _rtmptConnectionSettings = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlElement(ElementName = "rtmpTransport")]
        public RtmpTransportSettings RtmpTransportSettings
        {
            get { return _rtmpTransportSettings; }
            set { _rtmpTransportSettings = value; }
        }
	}

	#endregion RtmpServerSettings

	#region ThreadPoolSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class ThreadPoolSettings
	{
		private int _minWorkerThreads;
		private int _maxWorkerThreads;
		private int _idleTimeout;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public ThreadPoolSettings()
		{
			_minWorkerThreads = FluorineFx.Threading.ThreadPoolEx.DefaultMinWorkerThreads;
			_maxWorkerThreads = FluorineFx.Threading.ThreadPoolEx.DefaultMaxWorkerThreads;
			_idleTimeout = FluorineFx.Threading.ThreadPoolEx.DefaultIdleTimeout;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "minWorkerThreads")]
		public int MinWorkerThreads
		{
			get{return _minWorkerThreads;}
			set{_minWorkerThreads = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxWorkerThreads")]
		public int MaxWorkerThreads
		{
			get{return _maxWorkerThreads;}
			set{_maxWorkerThreads = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "idleTimeout")]
		public int IdleTimeout
		{
			get{return _idleTimeout;}
			set{_idleTimeout = value;}
		}
	}

	#endregion ThreadPoolSettings

    #region RtmpConnectionSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RtmpConnectionSettings
    {
        private int _pingInterval;
        private int _maxInactivity;
        private int _maxHandshakeTimeout;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public RtmpConnectionSettings()
        {
            _pingInterval = 5000;
            _maxInactivity = 60000;
            _maxHandshakeTimeout = 5000;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "pingInterval")]
        public int PingInterval
        {
            get { return _pingInterval; }
            set { _pingInterval = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxInactivity")]
        public int MaxInactivity
        {
            get { return _maxInactivity; }
            set { _maxInactivity = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxHandshakeTimeout")]
        public int MaxHandshakeTimeout
        {
            get { return _maxHandshakeTimeout; }
            set { _maxHandshakeTimeout = value; }
        }
    }

    #endregion RtmpConnectionSettings

    #region RtmptConnectionSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RtmptConnectionSettings
    {
        private int _pingInterval;
        private int _maxInactivity;
        private int _maxHandshakeTimeout;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public RtmptConnectionSettings()
        {
            _pingInterval = 5000;
            _maxInactivity = 60000;
            _maxHandshakeTimeout = 5000;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "pingInterval")]
        public int PingInterval
        {
            get { return _pingInterval; }
            set { _pingInterval = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxInactivity")]
        public int MaxInactivity
        {
            get { return _maxInactivity; }
            set { _maxInactivity = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxHandshakeTimeout")]
        public int MaxHandshakeTimeout
        {
            get { return _maxHandshakeTimeout; }
            set { _maxHandshakeTimeout = value; }
        }
    }

    #endregion RtmptConnectionSettings

    #region RtmpTransportSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RtmpTransportSettings
    {
        private int _receiveBufferSize;
        private int _sendBufferSize;
        private bool _tcpNoDelay;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public RtmpTransportSettings()
        {
            _receiveBufferSize = 4096;
            _sendBufferSize = 4096;
            _tcpNoDelay = true;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "receiveBufferSize")]
        public int ReceiveBufferSize
        {
            get { return _receiveBufferSize; }
            set { _receiveBufferSize = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "sendBufferSize")]
        public int SendBufferSize
        {
            get { return _sendBufferSize; }
            set { _sendBufferSize = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "boolean", AttributeName = "tcpNoDelay")]
        public bool TcpNoDelay
        {
            get { return _tcpNoDelay; }
            set { _tcpNoDelay = value; }
        }
    }

    #endregion RtmpTransportSettings

    #region OptimizerSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class OptimizerSettings
	{
		private string _provider;
		private bool _debug;
        private bool _typeCheck;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public OptimizerSettings()
		{
            _provider = "codedom";
			_debug = true;
            _typeCheck = false;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "provider")]
		public string Provider
		{
			get{return _provider;}
			set{_provider = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "boolean", AttributeName = "debug")]
		public bool Debug
		{
			get{return _debug;}
			set{_debug = value;}
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        //[XmlAttribute(DataType = "boolean", AttributeName = "typeCheck")]
        public bool TypeCheck
        {
            get { return _typeCheck; }
            set { _typeCheck = value; }
        }
	}

	#endregion OptimizerSettings

	#region SwxSettings

	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	public sealed class SwxSettings
	{
		private bool _allowDomain;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
		public SwxSettings()
		{
			_allowDomain = true;
		}
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "boolean", AttributeName = "allowDomain")]
		public bool AllowDomain
		{
			get{return _allowDomain;}
			set{_allowDomain = value;}
		}
	}

	#endregion SwxSettings

	#region JSonSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class JSonSettings
	{
        private JsonRpcGeneratorCollection _jsonRpcGeneratorCollection;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public JSonSettings()
        {
        }

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlArray("jsonRpcGenerators")]
        [XmlArrayItem("add", typeof(JsonRpcGenerator))]
        public JsonRpcGeneratorCollection JsonRpcGenerators
        {
            get 
            {
                if (_jsonRpcGeneratorCollection == null)
                    _jsonRpcGeneratorCollection = new JsonRpcGeneratorCollection();
                return _jsonRpcGeneratorCollection;
            }
            set { _jsonRpcGeneratorCollection = value; }
        }
    }

    #endregion JSonSettings

    #region JsonRpcGeneratorCollection

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class JsonRpcGeneratorCollection : CollectionBase
    {
        Hashtable _generators;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public JsonRpcGeneratorCollection()
        {
            _generators = new Hashtable();
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(JsonRpcGenerator value)
        {
            _generators.Add(value.Name, value);
            return List.Add(value);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(JsonRpcGenerator value)
        {
            return List.IndexOf(value);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void Insert(int index, JsonRpcGenerator value)
        {
            _generators.Add(value.Name, value);
            List.Insert(index, value);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(JsonRpcGenerator value)
        {
            _generators.Remove(value.Name);
            List.Remove(value);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(JsonRpcGenerator value)
        {
            return List.Contains(value);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            return _generators.Contains(name);
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonRpcGenerator this[int index]
        {
            get
            {
                return List[index] as JsonRpcGenerator;
            }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonRpcGenerator this[string name]
        {
            get
            {
                return _generators[name] as JsonRpcGenerator;
            }
        }
    }

    #endregion JsonRpcGeneratorCollection

    #region JsonRpcGenerator

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class JsonRpcGenerator
    {
        private string _name;
        private string _type;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public JsonRpcGenerator()
        {
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "string", AttributeName = "type")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }

    #endregion JsonRpcGenerator

    #region RuntimeSettings

    /// <summary>
    /// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
    /// </summary>
    public sealed class RuntimeSettings
    {
        private bool _asyncHandler;
        private int _minWorkerThreads;
        private int _maxWorkerThreads;

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public RuntimeSettings()
        {
            _asyncHandler = false;
            _minWorkerThreads = 0;
            _maxWorkerThreads = 0;
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "minWorkerThreads")]
        public int MinWorkerThreads
        {
            get { return _minWorkerThreads; }
            set { _minWorkerThreads = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "int", AttributeName = "maxWorkerThreads")]
        public int MaxWorkerThreads
        {
            get { return _maxWorkerThreads; }
            set { _maxWorkerThreads = value; }
        }
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        [XmlAttribute(DataType = "boolean", AttributeName = "asyncHandler")]
        public bool AsyncHandler
        {
            get { return _asyncHandler; }
            set { _asyncHandler = value; }
        }
    }

    #endregion RuntimeSettings

}