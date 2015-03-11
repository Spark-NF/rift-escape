#ifndef CYLINDERMAPPING_CGINC
#define CYLINDERMAPPING_CGINC

float Cylinder_Depth;
float Cylinder_Angle;
float Cylinder_Radius;

float4 MapCoordinate(float4 coord)
{
    float sx = 500;
    float theta = (coord.x / sx) * Cylinder_Angle;
    float radius = Cylinder_Radius * sx;
    float depth = Cylinder_Depth * sx;
    
    coord.x = sin(theta) * radius;
    coord.z = (cos(theta) * radius) + depth;
    
    return coord;
} 

#endif
