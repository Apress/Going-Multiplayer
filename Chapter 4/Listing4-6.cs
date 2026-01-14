//Before scrambling: 

class Gun 

{ 

    int ammo; //At +0 from Gun's start -> 0x11223300 

    int maxAmmo; //At +4 from Gun’s start -> 0x11223304 

} 

 

//After scrambling: 

class Gun 

{ 

    bool canFire; //At +0 from Gun's start -> 0x11223300 

    int ammo; //At +1 from Gun's start -> 0x11223301 

    int maxAmmo; //At +5 from Gun’s start 0x11223305 

} 