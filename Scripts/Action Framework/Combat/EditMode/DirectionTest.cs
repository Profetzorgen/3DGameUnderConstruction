using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DirectionTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void Test1() // testar om den skapar och kan l�gga till objektet vapen
    {
        var gameObject = new GameObject();
        gameObject.AddComponent<Weapon>();
        Assert.IsNotNull(gameObject);
    }
    [Test]
    public void HealthTest() // Kontrollerar funktioner f�r health
    {
        var h = new Health();
        Assert.AreEqual(h.CurrentHealth,h.MaxHealth);
    }
    [Test]
    public void WeaponTest() // Kollar att vapnets multiplier st�mmer med 1.0f
    {
        var w = new Weapon();
        Assert.AreEqual(w.DamageMultiplier, 1.0f);
    }
    [Test]
    public void GeneralVulnerableTest() // kollar att MaxHealth och CurrentHealth st�mmer
        // g�r dem det s� fungerar "take damage" funktionen mellan skripten!
    {
        var gV = new GeneralVulnerable();
        var gO = new GameObject();
        Assert.AreEqual(gV.MaxHealth, gV.CurrentHealth);
        Assert.IsNotNull(gO);
    }
    [Test]
   
    public void WASD() // Testar diverse grejer ur BrackeysWASD
    {
        var gO = new GameObject();
        gO.AddComponent<BrackeysWASD>(); // testar att den kan skapa en GameObject och att den f�r ett skript
        Assert.IsNotNull(gO);

        var wasd = new BrackeysWASD(); // skapar en ny skript f�r WASD
        wasd.Awake(); // k�r Awake och tilldelar allting deras namn samt h�mtar komponenter f�r testning
        Assert.IsNotNull(wasd.speed);
        Assert.IsNotNull(wasd.animator);
        Assert.IsNotNull(wasd.controller);
    }

}
