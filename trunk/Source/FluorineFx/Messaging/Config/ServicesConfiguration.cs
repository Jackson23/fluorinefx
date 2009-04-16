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
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using FluorineFx.Util;
using FluorineFx.Configuration;
using log4net;

namespace FluorineFx.Messaging.Config
{
    [XmlRootAttribute("services-config")]
    public sealed class ServicesConfiguration
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ServicesConfiguration));

        Services _services;

        [XmlElement("services")]
        public Services Services
        {
            get
            {
                if (_services == null)
                    _services = new Services();
                return _services;
            }
            set { _services = value; }
        }

        FlexClient _flexClient;

        [XmlElement("flex-client")]
        public FlexClient FlexClient
        {
            get
            {
                if (_flexClient == null)
                {
                    lock (typeof(ServicesConfiguration))
                    {
                        if (_flexClient == null)
                            _flexClient = new FlexClient();
                    }
                }
                return _flexClient; 
            }
            set { _flexClient = value; }
        }

        SecurityConfiguration _security;

        [XmlElement("security")]
        public SecurityConfiguration Security
        {
            get{ return _security; }
            set { _security = value; }
        }

        ChannelDefinition[] _channels;

        [XmlArray("channels")]
        [XmlArrayItem("channel-definition")]
        public ChannelDefinition[] Channels
        {
            get{ return _channels; }
            set { _channels = value; }
        }

        Factory[] _factories;

        [XmlArray("factories")]
        [XmlArrayItem("factory")]
        public Factory[] Factories
        {
            get { return _factories; }
            set { _factories = value; }
        }

        internal ServicesConfiguration()
        {
        }

        public static ServicesConfiguration Load(string path)
        {
            if (!File.Exists(path))
                return null;
            using (StreamReader streamReader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServicesConfiguration));
                ServicesConfiguration sc = serializer.Deserialize(streamReader) as ServicesConfiguration;
                streamReader.Close();
                string rootPath = Directory.GetParent(path).FullName;
                if (sc.Services != null && sc.Services.Includes != null)
                {
                    foreach (ServiceInclude include in sc.Services.Includes)
                        include.Load(sc, rootPath);
                }
                if (sc.Services != null && sc.Services.ServiceDefinitions != null)
                {
                    foreach (ServiceDefinition serviceDefinition in sc.Services.ServiceDefinitions)
                    {
                        serviceDefinition.Parent = sc;
                        if (serviceDefinition.Destinations != null)
                        {
                            foreach (DestinationDefinition destinationDefinition in serviceDefinition.Destinations)
                                destinationDefinition.Service = serviceDefinition;
                        }
                    }
                }
                return sc;
            }
        }

        public static ServicesConfiguration Load(string configPath, string configFileName)
        {
            string servicesConfigFile = Path.Combine(configPath, configFileName);
            return Load(servicesConfigFile);
        }

        public static ServicesConfiguration CreateDefault()
        {
            ServicesConfiguration sc = new ServicesConfiguration();

            LoginCommandCollection loginCommandCollection = FluorineConfiguration.Instance.LoginCommands;
            if (loginCommandCollection != null)
            {
                LoginCommand loginCommand = new LoginCommand();
                loginCommand.Class = loginCommandCollection.GetLoginCommand(LoginCommandSettings.FluorineLoginCommand);
                loginCommand.Server = LoginCommandSettings.FluorineLoginCommand;
                SecurityConfiguration securityConfiguration = new SecurityConfiguration();
                securityConfiguration.LoginCommands = new LoginCommand[] { loginCommand };
                sc.Security = securityConfiguration;
            }

            //Create a default amf channel
            ChannelDefinition channelDefinition = new ChannelDefinition();
            channelDefinition.Class = "flex.messaging.endpoints.AMFEndpoint";
            channelDefinition.Id = "my-amf";
            Endpoint endpoint = new Endpoint();
            endpoint.Url = @"http://{server.name}:{server.port}/{context.root}/Gateway.aspx";
            endpoint.Class = "flex.messaging.endpoints.AMFEndpoint";
            channelDefinition.Endpoint = endpoint;
            sc.Channels = new ChannelDefinition[] { channelDefinition };

            ServiceDefinition serviceDefinition = new ServiceDefinition(sc);
            serviceDefinition.Id = FluorineFx.Messaging.Services.RemotingService.RemotingServiceId;
            serviceDefinition.Class = typeof(FluorineFx.Messaging.Services.RemotingService).FullName;
            serviceDefinition.MessageTypes = "flex.messaging.messages.RemotingMessage";

            AdapterDefinition adapter = new AdapterDefinition();
            adapter.Id = "dotnet";
            adapter.Class = typeof(FluorineFx.Remoting.RemotingAdapter).FullName;
            adapter.Default = true;
            serviceDefinition.AddAdapter(adapter);
            serviceDefinition.Adapters = new AdapterDefinition[] { adapter };
            AdapterRef adapterRef = new AdapterRef();
            adapterRef.Ref = "dotnet";

            DestinationDefinition destination = new DestinationDefinition(serviceDefinition);
            destination.Id = DestinationDefinition.FluorineDestination;
            destination.AdapterRef = adapterRef;
            DestinationProperties properties = new DestinationProperties();
            properties.Source = "*";
            destination.Properties = properties;
            serviceDefinition.AddDestination(destination);

            Services services = new Services();
            services.ServiceDefinitions = new ServiceDefinition[] { serviceDefinition };
            sc.Services = services;

            FlexClient flexClient = new FlexClient();
            sc.FlexClient = flexClient;
            return sc;
        }

        public ServiceDefinition GetServiceByClass(string @class)
        {
            if (this.Services != null)
            {
                if (this.Services.ServiceDefinitions != null)
                {
                    foreach (ServiceDefinition serviceConfiguration in this.Services.ServiceDefinitions)
                    {
                        if (serviceConfiguration.Class == @class)
                            return serviceConfiguration;
                    }
                }
                if (this.Services.Includes != null)
                {
                    foreach (ServiceInclude include in this.Services.Includes)
                    {
                        if (include.ServiceDefinition.Class == @class)
                            return include.ServiceDefinition;
                    }
                }
            }
            return null;
        }

        public SecurityConstraint GetSecurityConstraintById(string id)
        {
            if (this.Security != null && this.Security.SecurityConstraints != null)
            {
                foreach (SecurityConstraint securityConstraint in this.Security.SecurityConstraints)
                {
                    if (securityConstraint.Id == id)
                        return securityConstraint;
                }
            }
            return null;
        }
    }

    public sealed class Factory
    {
        string _id;
        /// <summary>
        /// Gets the identity of the factory.
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }
    }
    /// <summary>
    /// The flex-client element of the services configuration file.
    /// </summary>
    public sealed class FlexClient
    {
        int _timeoutMinutes = 20;

        /// <summary>
        /// Gets or sets the number of minutes before an idle FlexClient is timed out.
        /// </summary>
        [XmlElement("timeout-minutes")]
        public int TimeoutMinutes
        {
            get { return _timeoutMinutes; }
            set { _timeoutMinutes = value; }
        }
    }

    public sealed class Services
    {
        ChannelRef[] _defaultChannels;

        [XmlArray("default-channels")]
        [XmlArrayItem("channel")]
        public ChannelRef[] DefaultChannels
        {
            get { return _defaultChannels; }
            set { _defaultChannels = value; }
        }

        ServiceDefinition[] _services;

        [XmlElement("service")]
        public ServiceDefinition[] ServiceDefinitions
        {
            get { return _services; }
            set { _services = value; }
        }

        ServiceInclude[] _includedServices;

        [XmlElement("service-include")]
        public ServiceInclude[] Includes
        {
            get { return _includedServices; }
            set { _includedServices = value; }
        }


        public void AddService(ServiceDefinition service)
        {
            if (_services == null)
                _services = new ServiceDefinition[1];
            else
                _services = ArrayUtils.Resize(_services, _services.Length + 1) as ServiceDefinition[];
            _services[_services.Length - 1] = service;
        }
    }

    public sealed class ServiceInclude
    {
        string _filePath;
        /// <summary>
        /// Gets the identity of the service.
        /// </summary>
        [XmlAttribute("file-path")]
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        ServiceDefinition _serviceDefinition;

        public ServiceDefinition ServiceDefinition
        {
            get { return _serviceDefinition; }
        }

        internal void Load(ServicesConfiguration parent, string rootPath)
        {
            _serviceDefinition = ServiceDefinition.Load(parent, Path.Combine(rootPath, this.FilePath));
        }
    }

    [XmlRootAttribute("service")]
    public sealed class ServiceDefinition
    {

        ServicesConfiguration _parent;
        static AdapterDefinition DefaultRemotingAdapterDefinition = new AdapterDefinition("dotnet", typeof(FluorineFx.Remoting.RemotingAdapter).FullName, true);

        [XmlIgnore]
        internal ServicesConfiguration Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        string _id;
        /// <summary>
        /// Gets the identity of the service.
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        
        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        string _messageTypes;

        [XmlAttribute("messageTypes")]
        public string MessageTypes
        {
            get { return _messageTypes; }
            set { _messageTypes = value; }
        }

        ChannelRef[] _defaultChannels;

        [XmlArray("default-channels")]
        [XmlArrayItem("channel")]
        public ChannelRef[] DefaultChannels
        {
            get { return _defaultChannels; }
            set { _defaultChannels = value; }
        }

        AdapterDefinition[] _adapters;

        [XmlArray("adapters")]
        [XmlArrayItem("adapter-definition")]
        public AdapterDefinition[] Adapters
        {
            get { return _adapters; }
            set { _adapters = value; }
        }

        DestinationDefinition[] _destinations;

        [XmlElement("destination")]
        public DestinationDefinition[] Destinations
        {
            get { return _destinations; }
            set { _destinations = value; }
        }

        internal ServiceDefinition()
        {
        }

        internal ServiceDefinition(ServicesConfiguration parent)
        {
            _parent = parent;
        }

        internal static ServiceDefinition Load(ServicesConfiguration parent, string path)
        {
            if (!File.Exists(path))
                return null;
            using (StreamReader streamReader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServiceDefinition));
                ServiceDefinition serviceDefinition = serializer.Deserialize(streamReader) as ServiceDefinition;
                serviceDefinition.Parent = parent;
                streamReader.Close();
                if (serviceDefinition.Destinations != null)
                {
                    foreach (DestinationDefinition destinationDefinition in serviceDefinition.Destinations)
                        destinationDefinition.Service = serviceDefinition;
                }
                return serviceDefinition;
            }
        }

        public AdapterDefinition GetAdapterByClass(string @class)
        {
            if (this.Adapters != null)
            {
                foreach (AdapterDefinition adapter in this.Adapters)
                {
                    if (adapter.Class == @class)
                        return adapter;
                }
            }
            return null;
        }

        public AdapterDefinition GetAdapterByRef(string @ref)
        {
            if (this.Adapters != null)
            {
                foreach (AdapterDefinition adapter in this.Adapters)
                {
                    if (adapter.Id == @ref)
                        return adapter;
                }
            }
            return null;
        }

        public AdapterDefinition GetDefaultAdapter()
        {
            if (this.Adapters != null)
            {
                foreach (AdapterDefinition adapter in this.Adapters)
                {
                    if (adapter.Default)
                        return adapter;
                }
            }
            else
            {
                return DefaultRemotingAdapterDefinition;
            }
            return null;
        }

        public void AddAdapter(AdapterDefinition adapter)
        {
            if (_adapters == null)
                _adapters = new AdapterDefinition[1];
            else
                _adapters = ArrayUtils.Resize(_adapters, _adapters.Length + 1) as AdapterDefinition[];
            _adapters[_adapters.Length - 1] = adapter;
        }

        public void AddDestination(DestinationDefinition destination)
        {
            if (_destinations == null)
                _destinations = new DestinationDefinition[1];
            else
                _destinations = ArrayUtils.Resize(_destinations, _destinations.Length + 1) as DestinationDefinition[];
            _destinations[_destinations.Length - 1] = destination;
        }

        /// <summary>
        /// Returns whether this Service is capable of handling messages of a given type.
        /// </summary>
        /// <param name="@class">The message type.</param>
        /// <returns>true if the Service is capable of handling messages of a given type; otherwise, false.</returns>
        public bool IsSupportedMessageType(string @class)
        {
            if (this.MessageTypes != null)
            {
                string[] messageTypesList = this.MessageTypes.Split(new char[] { ',' });
                foreach (string messageType in messageTypesList)
                {
                    if (messageType == @class)
                        return true;
                    string type = FluorineConfiguration.Instance.ClassMappings.GetType(messageType);
                    if (type == @class)
                        return true;
                }
            }
            return false;
        }
    }

    public sealed class SecurityConfiguration
    {
        SecurityConstraint[] _securityConstraints;

        [XmlElement("security-constraint")]
        public SecurityConstraint[] SecurityConstraints
        {
            get { return _securityConstraints; }
            set { _securityConstraints = value; }
        }

        LoginCommand[] _loginCommands;

        [XmlElement("login-command")]
        public LoginCommand[] LoginCommands
        {
            get { return _loginCommands; }
            set { _loginCommands = value; }
        }

        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public LoginCommand GetLoginCommand(string server)
        {
            if (this.LoginCommands != null)
            {
                foreach (LoginCommand loginCommand in this.LoginCommands)
                    if (loginCommand.Server == server)
                        return loginCommand;
            }
            return null;
        }
    }

    public sealed class AdapterDefinition
    {
        string _id;
        /// <summary>
        /// Gets the identity of the adapter.
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        bool _default;

        [XmlAttribute("default")]
        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }

        internal AdapterDefinition()
        {
        }

        internal AdapterDefinition(string id, string @class, bool @default)
        {
            _id = id;
            _class = @class;
            _default = @default;
        }

    }

    public sealed class AdapterRef
    {
        string _ref;

        [XmlAttribute("ref")]
        public string Ref
        {
            get { return _ref; }
            set { _ref = value; }
        }

        public AdapterRef()
        {
        }

        public AdapterRef(AdapterDefinition adapter)
        {
            _ref = adapter.Id;
        }
    }

    /// <summary>
    /// Contains the properties for configuring service adapters.
    /// This is the <b>destination</b> element in the services-config.xml file.
    /// </summary>
    public sealed class DestinationDefinition
    {
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string FluorineDestination = "fluorine";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string FluorineServiceBrowserDestination = "FluorineFx.ServiceBrowser.FluorineServiceBrowser";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string FluorineManagementDestination = "FluorineFx.ServiceBrowser.ManagementService";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string FluorineCodeGeneratorDestination = "FluorineFx.ServiceBrowser.CodeGeneratorService";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string FluorineSqlServiceDestination = "FluorineFx.ServiceBrowser.SqlService";

        string[] _cachedRoles;
        static string[] EmptyRoles = new string[0];

        ServiceDefinition _service;

        [XmlIgnore]
        internal ServiceDefinition Service
        {
            get { return _service; }
            set { _service = value; }
        }

        string _id;
        /// <summary>
        /// Gets the identity of the destination.
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        AdapterRef _adapterRef;

        [XmlElement("adapter")]
        public AdapterRef AdapterRef
        {
            get { return _adapterRef; }
            set { _adapterRef = value; }
        }

        ChannelRef[] _channels;

        [XmlArray("channels")]
        [XmlArrayItem("channel")]
        public ChannelRef[] Channels
        {
            get { return _channels; }
            set { _channels = value; }
        }

        /*
        DestinationProperties _properties;

        [XmlElement("properties")]
        public DestinationProperties Properties
        {
            get 
            {
                if (_properties == null)
                    _properties = new DestinationProperties();
                return _properties; 
            }
            set { _properties = value; }
        }
        */
        
        private XmlNode _propertiesXml;

        [XmlAnyElement("properties")]
        public XmlNode PropertiesXml
        {
            get { return _propertiesXml; }
            set { _propertiesXml = value; }
        }

        DestinationProperties _properties;
        [XmlIgnore]
        public DestinationProperties Properties
        {
            get
            {
                if (_properties == null)
                {
                    lock (typeof(DestinationProperties))
                    {
                        if (_properties == null)
                        {
                            if (_propertiesXml != null)
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(DestinationProperties));
                                StringReader stringReader = new StringReader(_propertiesXml.OuterXml);
                                using (XmlTextReader xmlReader = new XmlTextReader(stringReader))
                                {
                                    _properties = serializer.Deserialize(xmlReader) as DestinationProperties;
                                }

                            }
                            else
                                _properties = new DestinationProperties();
                        }
                    }
                }
                return _properties;
            }
            set
            {
                //When initializing with default values
                _properties = value;
            }
        }


        SecurityConstraint[] _securityConstraints;

        [XmlArray("security")]
        [XmlArrayItem("security-constraint")]
        public SecurityConstraint[] Security
        {
            get { return _securityConstraints; }
            set { _securityConstraints = value; }
        }

        internal DestinationDefinition()
        {
        }

        internal DestinationDefinition(ServiceDefinition service)
        {
            _service = service;
        }

        public string[] GetRoles()
        {
            if (_cachedRoles == null)
            {
                Debug.Assert(this.Service != null);
                if (this.Security != null)
                {
                    ArrayList result = new ArrayList();
                    foreach (SecurityConstraint securityConstraint in this.Security)
                    {
                        SecurityConstraint constraint;
                        if (securityConstraint.Ref == null)
                            constraint = securityConstraint;
                        else
                            constraint = this.Service.Parent.GetSecurityConstraintById(securityConstraint.Ref);
                        if (constraint != null)
                        {
                            if (constraint.Roles != null)
                                result.AddRange(constraint.Roles);
                        }
                    }
                    if (result.Count > 0)
                        _cachedRoles = result.ToArray(typeof(string)) as string[];
                }
                if (_cachedRoles == null)
                    _cachedRoles = DestinationDefinition.EmptyRoles;
            }
            return _cachedRoles;
        }
    }

    [XmlRoot("properties")]
    public sealed class DestinationProperties
    {
        string _source;

        [XmlElement("source")]
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        string _scope;

        [XmlElement("scope")]
        public string Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }

        string _factory;

        [XmlElement("factory")]
        public string Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        string _attributeId;

        [XmlElement("attribute-id")]
        public string AttributeId
        {
            get { return _attributeId; }
            set { _attributeId = value; }
        }

        NetworkProperties _networkProperties;

        [XmlElement("network")]
        public NetworkProperties Network
        {
            get { return _networkProperties; }
            set { _networkProperties = value; }
        }

        MetadataProperties _metadataProperties;

        [XmlElement("metadata")]
        public MetadataProperties Metadata
        {
            get { return _metadataProperties; }
            set { _metadataProperties = value; }
        }

        MsmqProperties _msmqProperties;

        [XmlElement("msmq")]
        public MsmqProperties Msmq
        {
            get { return _msmqProperties; }
            set { _msmqProperties = value; }
        }

        ServerProperties _serverProperties;

        [XmlElement("server")]
        public ServerProperties Server
        {
            get { return _serverProperties; }
            set { _serverProperties = value; }
        }
    }

    /// <summary>
    /// Network policy settings for a MessageDestination.
    /// </summary>
    public sealed class NetworkProperties
    {
        int _sessionTimeout = 20;

        /// <summary>
        /// The session-timeout element specifies the idle time in minutes before a subscriber is unsubscribed. 
        /// When you set the value to 0 (zero), subscribers are not forced to unsubscribe automatically. The default value is 20.
        /// </summary>
        [XmlElement("session-timeout")]
        public int SessionTimeout
        {
            get { return _sessionTimeout; }
            set { _sessionTimeout = value; }
        }

        PagingProperties _paging;

        [XmlElement("paging")]
        public PagingProperties Paging
        {
            get { return _paging; }
            set { _paging = value; }
        }
    }

    public sealed class PagingProperties
    {
        int _pageSize = 10;

        /// <summary>
        /// The pageSize attribute indicates the number of records to be sent to the client when the client-side DataService.fill() method is called.
        /// </summary>
        [XmlElement("pageSize")]
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        bool _enabled;

        /// <summary>
        /// The enabled attribute of the paging element indicates whether data paging is enabled for the destination.
        /// </summary>
        [XmlElement("enabled")]
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
    }

    /// <summary>
    /// Contains the properties for configuring destination metadata.
    /// This is the <b>metadata</b> element in the services-config.xml file.
    /// </summary>
    public sealed class MetadataProperties
    {
        IdentityConfiguration[] _identity;

        [XmlElement("identity")]
        public IdentityConfiguration[] Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
    }

    public sealed class IdentityConfiguration
    {
        internal static IdentityConfiguration[] Empty = new IdentityConfiguration[0];

        string _property;

        [XmlAttribute("property")]
        public string Property
        {
            get { return _property; }
            set { _property = value; }
        }
    }

    public sealed class MsmqProperties
    {
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string BinaryMessageFormatter = "BinaryMessageFormatter";
        /// <summary>
        /// This member supports the Fluorine infrastructure and is not intended to be used directly from your code.
        /// </summary>
        public const string XmlMessageFormatter = "XmlMessageFormatter";

        string _name;

        [XmlElement("name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        string _label;

        [XmlElement("label")]
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }

        string _formatter;

        [XmlElement("formatter")]
        public string Formatter
        {
            get { return _formatter; }
            set { _formatter = value; }
        }
    }

    /// <summary>
    /// Contains the properties for configuring server settings for message destinations.
    /// This is the <b>server</b> element in the services-config.xml file.
    /// Server settings for a message destination.
    /// </summary>
    public sealed class ServerProperties
    {
        bool _allowSubtopics;

        /// <summary>
        /// Gets whether subtopics are allowed.
        /// </summary>
        [XmlElement("allow-subtopics")]
        public bool AllowSubtopics
        {
            get { return _allowSubtopics; }
            set { _allowSubtopics = value; }
        }
    }

    /// <summary>
    /// Contains the properties for declaring a security constraint inline(destination) or globally.
    /// Security constraints are used by the login manager to secure access to destinations and endpoints.
    /// This is the <b>security-constraint</b> element in the services-config.xml file.
    /// </summary>
    public sealed class SecurityConstraint
    {
        string _id;
        /// <summary>
        /// Gets the identity of the security constraint.
        /// </summary>
        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _authMethod;
        /// <summary>
        /// Gets the authentication method of the security constraint.
        /// </summary>
        [XmlElement("auth-method")]
        public string AuthMethod
        {
            get { return _authMethod; }
            set { _authMethod = value; }
        }

        string _ref;

        [XmlAttribute("ref")]
        public string Ref
        {
            get { return _ref; }
            set { _ref = value; }
        }

        string[] _roles;
        /// <summary>
        /// Gets the role memberships of the security constraint.
        /// </summary>
        [XmlArray("roles")]
        [XmlArrayItem("role")]
        public string[] Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }

        public SecurityConstraint()
        {
        }

        internal SecurityConstraint(string id, string authMethod, string[] roles)
        {
            _id = id;
            _authMethod = authMethod;
            _roles = roles;
        }
    }

    public sealed class LoginCommand
    {
        public const string FluorineLoginCommand = "asp.net";

        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        string _server;

        [XmlAttribute("server")]
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        bool _perClientAuthentication = false;
        /// <summary>
        /// Set to true to enable per-client authentication. The default value is false.
        /// </summary>
        [XmlElement("per-client-authentication")]
        public bool IsPerClientAuthentication
        {
            get { return _perClientAuthentication; }
            set { _perClientAuthentication = value; }
        }
    }

    public sealed class ChannelRef
    {
        string _ref;

        [XmlAttribute("ref")]
        public string Ref
        {
            get { return _ref; }
            set { _ref = value; }
        }
    }

    /// <summary>
    /// Contains the properties for configuring message channels.
    /// This is the <b>channel-definition</b> element in the services-config.xml file.
    /// </summary>
    public sealed class ChannelDefinition
    {
        /// <summary>
        /// Context Root token.
        /// </summary>
        public const string ContextRoot = "{context.root}";

        string _id;

        [XmlAttribute("id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }

        Endpoint _endpoint;

        [XmlElement("endpoint")]
        public Endpoint Endpoint
        {
            get{ return _endpoint; }
            set { _endpoint = value; }
        }

        ChannelProperties _properties;

        [XmlElement("properties")]
        public ChannelProperties Properties
        {
            get 
            {
                if (_properties == null)
                {
                    lock (typeof(ChannelProperties))
                    {
                        if (_properties == null)
                            _properties = new ChannelProperties();
                    }
                }
                return _properties; 
            }
            set { _properties = value; }
        }

        UriBase _uri;

        /// <summary>
        /// Returns the endpoint URI of the channel definition.
        /// </summary>
        /// <returns>The endpoint URI representation of the channel definition.</returns>
        public UriBase GetUri()
        {
            if (_uri == null)
            {
                if (this.Endpoint != null)
                {
                    if( this.Endpoint.Url != null )
                        _uri = new UriBase(this.Endpoint.Url);
                    if (this.Endpoint.Uri != null)
                        _uri = new UriBase(this.Endpoint.Uri);
                }
            }
            return _uri;
        }

        internal bool Bind(string path, string contextPath)
        {
            // The context root maps requests to the Flex application.
            // For example, the context root in the following URL is /flex:
            // http://localhost:8700/flex/myApp.mxml
            //
            // In the Flex configuration files, the {context.root} token takes the place of 
            // the path to the Flex web application itself. If you are running your MXML apps 
            // inside http://localhost:8100/flex) then "/flex" is the {context.root}. 
            // The value of {context.root} includes the prefix "/". 
            // As a result, you are not required to add a forward slash before the {context.root} token.
            //
            // If {context.root} is used in a nonrelative path, it must not have a leading "/". 
            // For example, instead of this:
            // http://localhost/{context.root}
            // Do this:
            // http://localhost{context.root}

            UriBase uri = GetUri();
            if (uri != null)
            {
                string endpointPath = uri.Path;
                if (!endpointPath.StartsWith("/"))
                    endpointPath = "/" + endpointPath;
                if (contextPath == "/")
                    contextPath = string.Empty;
                if (endpointPath.IndexOf("/" + ChannelDefinition.ContextRoot) != -1)
                {
                    //relative path
                    endpointPath = endpointPath.Replace("/" + ChannelDefinition.ContextRoot, contextPath);
                }
                else
                {
                    //nonrelative path, but we do not handle these for now
                    endpointPath = endpointPath.Replace(ChannelDefinition.ContextRoot, contextPath);
                }
                if (endpointPath.ToLower() == path.ToLower())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Returns a String that represents the current channel settings.
        /// </summary>
        /// <returns>A String that represents the current ChannelSettings.</returns>
        public override string ToString()
        {
            if (_uri != null)
                return "Channel id = " + _id + " uri: " + _uri.Uri + " endpointPath: " + _uri.Path;
            else
                return "Channel id = " + _id + " (uri not available)";
        }
    }

    public sealed class Endpoint
    {
        string _url;

        [XmlAttribute("url")]
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        string _uri;

        [XmlAttribute("uri")]
        public string Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        string _class;

        [XmlAttribute("class")]
        public string Class
        {
            get { return _class; }
            set { _class = value; }
        }
    }

    public sealed class ChannelProperties
    {
        bool _pollingEnabled = false;

        [XmlElement("polling-enabled")]
        public bool IsPollingEnabled
        {
            get { return _pollingEnabled; }
            set { _pollingEnabled = value; }
        }

        int _pollingIntervalSeconds = 8;
        /// <summary>
        /// Gets or sets the polling interval.
        /// </summary>
        [XmlElement("polling-interval-seconds")]
        public int PollingIntervalSeconds
        {
            get { return _pollingIntervalSeconds; }
            set { _pollingIntervalSeconds = value; }
        }

        int _pollingIntervalMillis = 3000;
        /// <summary>
        /// Optional channel property. Default value is 3000. This parameter specifies the number of milliseconds the client waits before polling the server again. 
        /// When polling-interval-millis is 0, the client polls as soon as it receives a response from the server with no delay.
        /// </summary>
        [XmlElement("polling-interval-millis")]
        public int PollingIntervalMillis
        {
            get { return _pollingIntervalMillis; }
            set { _pollingIntervalMillis = value; }
        }

        int _waitIntervalMillis = 0;
        /// <summary>
        /// Optional endpoint property. Default value is 0. This parameter specifies the number of milliseconds the server poll response thread waits 
        /// for new messages to arrive when the server has no messages for the client at the time of poll request handling. 
        /// For this setting to take effect, you must use a nonzero value for the max-waiting-poll-requests property.
        /// 
        /// A value of 0 means that server does not wait for new messages for the client and returns an empty acknowledgment as usual. 
        /// A value of -1 means that server waits indefinitely until new messages arrive for the client before responding to the client poll request.
        /// The recommended value is 60000 milliseconds (one minute).
        /// </summary>
        [XmlElement("wait-interval-millis")]
        public int WaitIntervalMillis
        {
            get { return _waitIntervalMillis; }
            set { _waitIntervalMillis = value; }
        }

        int _maxWaitingPollRequests = 0;
        /// <summary>
        /// Optional endpoint property. Default value is 0. Specifies the maximum number of server poll response threads that can be in wait state. 
        /// When this limit is reached, the subsequent poll requests are treated as having zero wait-interval-millis.
        /// </summary>
        [XmlElement("max-waiting-poll-requests")]
        public int MaxWaitingPollRequests
        {
            get { return _maxWaitingPollRequests; }
            set { _maxWaitingPollRequests = value; }
        }

        int _serverToClientHeartbeatMillis = 5000;
        /// <summary>
        /// Optional endpoint property. Default value is 5000. Number of milliseconds that the server waits before writing a single byte to the 
        /// streaming connection to make sure that the client is still available. 
        /// This is important to determine when a client is no longer available so that its resources associated with the streaming connection 
        /// can be cleaned up. 
        /// Note that this functionality keeps the session alive. A non-positive value disables this functionality.
        /// </summary>
        [XmlElement("server-to-client-heartbeat-millis")]
        public int ServerToClientHeartbeatMillis
        {
            get { return _serverToClientHeartbeatMillis; }
            set { _serverToClientHeartbeatMillis = value; }
        }

        int _connectTimeoutSeconds = 5;
        /// <summary>
        /// Using a streaming connection that passes through an HTTP 1.1 proxy server that incorrectly buffers the response sent back to the 
        /// client hangs the connection. For this reason, you must set the connect-timeout-seconds property to a relatively short timeout 
        /// period and specify a fallback channel such as an AMF polling channel.
        /// </summary>
        [XmlElement("connect-timeout-seconds")]
        public int ConnectTimeoutSeconds
        {
            get { return _connectTimeoutSeconds; }
            set { _connectTimeoutSeconds = value; }
        }

        int _idleTimeoutMinutes = 0;
        /// <summary>
        /// Optional channel property. Default value is 0. Specifies the number of minutes that a streaming channel is allowed to remain idle 
        /// before it is closed. Setting the idle-timeout-minutes property to 0 disables the timeout completely, but it is a potential security concern.
        /// </summary>
        [XmlElement("idle-timeout-minutes")]
        public int IdleTimeoutMinutes
        {
            get { return _idleTimeoutMinutes; }
            set { _idleTimeoutMinutes = value; }
        }

        int _maxStreamingClients = 10;
        /// <summary>
        /// Optional endpoint property. Default value is 10. Limits the number of Flex clients that can open a streaming connection to the endpoint. 
        /// To determine an appropriate value, consider the number of threads available on your application server because each streaming connection 
        /// open between a Client and the streaming endpoints uses a thread on the server. 
        /// Use a value that is lower than the maximum number of threads available on the application server.
        /// </summary>
        [XmlElement("max-streaming-clients")]
        public int MaxStreamingClients
        {
            get { return _maxStreamingClients; }
            set { _maxStreamingClients = value; }
        }

        string _bindAddress;
        /// <summary>
        /// Gets the network interface address to bind the RTMP channel to.
        /// </summary>
        [XmlElement("bind-address")]
        public string BindAddress
        {
            get { return _bindAddress; }
            set { _bindAddress = value; }
        }

        Serialization _serialization;
        [XmlElement("serialization")]
        public Serialization Serialization
        {
            get { return _serialization; }
            set { _serialization = value; }
        }

        UserAgentSettings _userAgentSettings;
        [XmlElement("user-agent-settings")]
        public UserAgentSettings UserAgentSettings
        {
            get 
            {
                if (_userAgentSettings == null)
                {
                    lock (typeof(UserAgentSettings))
                    {
                        if (_userAgentSettings == null)
                            _userAgentSettings = new UserAgentSettings();
                        if (_userAgentSettings[UserAgent.UserAgentMSIE.MatchOn] == null)
                            _userAgentSettings.AddUserAgent(UserAgent.UserAgentMSIE);
                        if (_userAgentSettings[UserAgent.UserAgentFirefox.MatchOn] == null)
                            _userAgentSettings.AddUserAgent(UserAgent.UserAgentFirefox);
                    }
                }
                return _userAgentSettings; 
            }
            set { _userAgentSettings = value; }
        }

        string _keystoreFile;
        /// <summary>
        /// If the channel requires a digital certificate this element specifies a keystore filename.
        /// </summary>
        [XmlElement("keystore-file")]
        public string KeystoreFile
        {
            get { return _keystoreFile; }
            set { _keystoreFile = value; }
        }

        string _keystorePassword;
        /// <summary>
        /// If the channel requires a digital certificate this element specifies a keystore password.
        /// </summary>
        [XmlElement("keystore-password")]
        public string KeystorePassword
        {
            get { return _keystorePassword; }
            set { _keystorePassword = value; }
        }

        ServerCertificate _serverCertificate;
        /// <summary>
        /// Specifies the name of the X.509 certificate store to open.
        /// </summary>
        [XmlElement("serverCertificate")]
        public ServerCertificate ServerCertificate
        {
            get { return _serverCertificate; }
            set { _serverCertificate = value; }
        }
    }

    /// <summary>
    /// Example: <serverCertificate storeLocation="LocalMachine" storeName="My" x509FindType="FindBySubjectDistinguishedName" findValue="CN=MyCert"/>
    /// </summary>
    public sealed class ServerCertificate
    {
        string _storeLocation;

        [XmlAttribute("storeLocation")]
        public string StoreLocation
        {
            get { return _storeLocation; }
            set { _storeLocation = value; }
        }

        string _storeName;

        [XmlAttribute("storeName")]
        public string StoreName
        {
            get { return _storeName; }
            set { _storeName = value; }
        }

        string _x509FindType;

        [XmlAttribute("x509FindType")]
        public string X509FindType
        {
            get { return _x509FindType; }
            set { _x509FindType = value; }
        }

        string _findValue;

        [XmlAttribute("findValue")]
        public string FindValue
        {
            get { return _findValue; }
            set { _findValue = value; }
        }
    }

    public sealed class UserAgentSettings
    {
        UserAgent[] _userAgents;

        [XmlElement("user-agent")]
        public UserAgent[] UserAgents
        {
            get { return _userAgents; }
            set { _userAgents = value; }
        }

        public UserAgent this[string value]
        {
            get
            {
                if( _userAgents == null )
                    return null;
                foreach (UserAgent userAgent in _userAgents)
                    if (userAgent.MatchOn == value)
                        return userAgent;
                return null;
            }
        }

        public void AddUserAgent(UserAgent userAgent)
        {
            if (_userAgents == null)
                _userAgents = new UserAgent[1];
            else
                _userAgents = ArrayUtils.Resize(_userAgents, _userAgents.Length + 1) as UserAgent[];
            _userAgents[_userAgents.Length - 1] = userAgent;
        }
    }

    public sealed class Serialization
    {
        bool _legacyCollection;

        [XmlElement("legacy-collection")]
        public bool IsLegacyCollection
        {
            get { return _legacyCollection; }
            set { _legacyCollection = value; }
        }

        bool _legacyThrowable;

        /// <summary>
        /// Gets whether Exception instances are serialized as AMF status-info objects by default.
        /// </summary>
        [XmlElement("legacy-throwable")]
        public bool IsLegacyThrowable
        {
            get { return _legacyThrowable; }
            set { _legacyThrowable = value; }
        }
    }

    public sealed class UserAgent
    {
        public static UserAgent UserAgentMSIE = new UserAgent("IE", 2048, 3);
        public static UserAgent UserAgentFirefox = new UserAgent("Firefox", 0, 7);

        string _matchOn;

        [XmlAttribute("match-on")]
        public string MatchOn
        {
            get { return _matchOn; }
            set { _matchOn = value; }
        }

        int _kickstartBytes;

        [XmlAttribute("kickstart-bytes")]
        public int KickstartBytes
        {
            get { return _kickstartBytes; }
            set { _kickstartBytes = value; }
        }

        int _maxStreamingConnectionsPerSession;

        [XmlAttribute("max-streaming-connections-per-session")]
        public int MaxStreamingConnectionsPerSession
        {
            get { return _maxStreamingConnectionsPerSession; }
            set { _maxStreamingConnectionsPerSession = value; }
        }

        public UserAgent()
        {
            _kickstartBytes = 0;
            _maxStreamingConnectionsPerSession = 1;
        }

        public UserAgent(string matchOn, int kickstartBytes, int maxStreamingConnectionsPerSession)
        {
            _matchOn = matchOn;
            _kickstartBytes = kickstartBytes;
            _maxStreamingConnectionsPerSession = maxStreamingConnectionsPerSession;
        }
    }
}
