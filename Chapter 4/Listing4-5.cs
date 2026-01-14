class Gun //the object itself stats at 0x11223300 
{ 
    int ammo; //starts at Gun's address + the size of the object pointer in a 64-bit operating system (8 bytes) 
    int maxAmmo; //starts at ammo's address + the size of ammo (4 bytes) 
} 