using Opc.UaFx;

namespace Dtos;

[OpcDataType(nameof(CustomDataType), 2)]
[OpcDataTypeEncoding(nameof(CustomDataType) + "-Binary", 2, Type = OpcEncodingType.Binary)]
public record CustomDataType(string Id) {
    public CustomDataType() : this(string.Empty) { }
}