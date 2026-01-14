... 

JMP MyCave // jump to custom code 

... 

mov edi, eax // do the original behavior 

ret 

MyCave: <custom code> // do what I want 

JMP <address of ret of original function> // jump back to the original function 

... 

//C# 

void SetPower(int power) 

{ 

    MyCave(power); 

} 

 

void MyCave(int power) 

{ 

    this.power = power; 

    // party hard here, I.E: auto-cast your most powerful skill. 

} 