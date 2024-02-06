﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Logger.Tests;

public class TestEntity : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = "";
}

public class TestStorage
{
    [Fact]
    public void StorageAdd_ContainsAdded()
    {
        Storage storage = new();
        TestEntity entity = new()
        {
            Name = "Name"
        };
        storage.Add(entity);
        Assert.True(storage.Contains(entity));
    }

    [Fact]
    public void StorageAddRemove_NotContains()
    {
        Storage storage = new();
        TestEntity entity = new()
        {
            Name = "Name"
        };
        storage.Add(entity);
        Assert.True(storage.Contains(entity));
        storage.Remove(entity);
        Assert.False(storage.Contains(entity));
    }

    [Fact]
    public void StorageContains_ContainsGuid()
    {
        Storage storage = new();
        Guid guid = Guid.NewGuid();
        TestEntity entity = new()
        {
            Name = "Name",
            Id = guid
        };
        TestEntity entity2 = new()
        {
            Name = "Name",
        };
        storage.Add(entity);
        storage.Add(entity2);
        Assert.True(storage.Contains(entity));
        Assert.True(storage.Contains(entity2));
        IEntity gotEntity = storage.Get(guid)!;
        Assert.NotNull(gotEntity);
        Assert.Equal(gotEntity.Id, guid);
    }
}
