import math

class Vector3: 

    def __init__(self, x: float, y : float, z : float) : 
        self.x = x
        self.y = y
        self.z = z
    
    def __add__(self, other) : 
        return Vector3(self.x + other.x, self.y + other.y, self.z + other.z)
    
    def __sub__(self, other) : 
        return Vector3(self.x - other.x, self.y - other.y, self.z - other.z)

    def __mul__(self, other) : 
        return (self.x * other.x) + (self.y * other.y) + (self.z * other.z)

    def abs(self) : 
        return math.sqrt(self.x**2 + self.y**2 + self.z**2)
    
    def v3_multiply(a, b) :
        x, y, z = a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y -  a.y * b.x
        return Vector3(x, y, z) 
    
    def v3_mixed_multiply(a, b, c) :
        return (a.x * b.y * c.z) + (c.x * a.y * b.z) + (b.x * c.y * x.z) - (c.x * b.y * a.z) - (a.x * c.y * b.z) - (b.y * a.y  * c.z)

    def v3_dist(a, b) : 
        return  math.sqrt((a.x - b.x)**2 + (a.y - b.y)**2 + (a.z - b.z)**2)

    def v3_angle(a, b) :
        return math.acos((a*b)/(a.abs() * b.abs()))

    def __repr__(self) : 
        return f"({self.x}, {self.y}, {self.z})"

def square(points: list) :
    if len(points) < 3 :
        return -1 
    ref_point, square = points[0], 0 
    for i in range(1,len(points) - 1):
        point =  Vector3.v3_multiply(points[i] - ref_point, points[i+1] - ref_point)
        if point.z < 0 : 
            square +=  point.abs()
        else :
            square -=  point.abs()
    return abs(square /2)


n, points = int(input().strip()), []
for _ in range(n):
    x, y = map(int, input().strip().split())
    points.append(Vector3(x, y, 0))

print ("{:.1f}".format(square(points))) 
