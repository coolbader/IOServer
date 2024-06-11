namespace IOServer.Logic.Models;
using System;
using System.Text.Encodings;
[GenerateSerializer]
public  class DataPoint
{
    [Id(0)]
    public string Id { get; set; }

    [Id(1)]
    public string Code { get; set; }

    [Id(2)]
    public int QL { get; set; }

    [Id(3)]
    public string Value  { get; set; }

    [Id(4)]
    public string Address { get; set; }

    [Id(5)]
    public long Ts { get; set; }

}
