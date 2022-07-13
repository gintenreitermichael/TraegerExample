using Opc.UaFx;

namespace TraegerExample.Console.Dtos {
  [OpcDataType(nameof(ResultCode), 2, Description = "Result code", UseDataTypeDescription = true)]
  public enum ResultCode {
    [OpcEnumMember(nameof(ErrorNoExampleId))]
    ErrorNoExampleId = 0,
    [OpcEnumMember(nameof(ErrorNoExampleName))]
    ErrorNoExampleName = 1,
    [OpcEnumMember(nameof(Success))]
    Success = 2,
  }
}