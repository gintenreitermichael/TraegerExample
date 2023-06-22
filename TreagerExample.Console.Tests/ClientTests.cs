﻿using System.Reflection;
using Opc.UaFx;
using Opc.UaFx.Client;
using Server;
using TraegerExample.Console;
using Xunit.Abstractions;

namespace TestProject1;

public class ClientTests : IDisposable
{
    private readonly ITestOutputHelper _output;
    private readonly OpcClient _client;
    private readonly OpcUaServer _server;
    
    public ClientTests(ITestOutputHelper output)
    {
        _output = output;
        _server = new OpcUaServer(Default.ServerAddress, new OpcUaNodeManager(Default.ServerAddress));
        _server.Start();

        _client = new OpcClient(Default.ServerAddress);
        _client.Connect();
    }

    [Fact]
    public void Try_Read_Node_Internal()
    {
        var typeInfo = typeof(CustomDataType).GetTypeInfo();
        _output.WriteLine(typeInfo.AssemblyQualifiedName);

        foreach (var attribute in typeInfo.GetCustomAttributes())
        {
            _output.WriteLine(attribute.GetType().Name);
        }
        
        var node = _client.ReadNode("ns=2;s=Data/LicenseOption");
        var licenseOption = node.As<CustomDataType>();
        Assert.NotNull(licenseOption);
        
        _output.WriteLine(licenseOption.ToString());
    }
    
    [Fact]
    public void Try_Read_Node_Dtos()
    {
        var typeInfo = typeof(Dtos.CustomDataType).GetTypeInfo();
        _output.WriteLine(typeInfo.AssemblyQualifiedName);

        foreach (var attribute in typeInfo.GetCustomAttributes())
        {
            _output.WriteLine(attribute.GetType().Name);
        }
        
        var node = _client.ReadNode("ns=2;s=Data/LicenseOption");
        var licenseOption = node.As<Dtos.CustomDataType>();
        Assert.NotNull(licenseOption);
        
        _output.WriteLine(licenseOption.ToString());
    }
    
    public void Dispose()
    {
        _client.Disconnect();
        _client.Dispose();
        
        _server.Dispose();
    }
}

[OpcDataType(nameof(CustomDataType), 2)]
[OpcDataTypeEncoding(nameof(CustomDataType) + "-Binary", 2, Type = OpcEncodingType.Binary)]
internal record CustomDataType(string Id) {
    public CustomDataType() : this(string.Empty) { }
}