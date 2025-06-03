﻿namespace ParkingOnline.Core.Entities;

public class Vaga : BaseEntity<int>
{
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}