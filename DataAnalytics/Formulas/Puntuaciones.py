import math
import sys
def miradaAUsuarios(t1, t2, t3):
    return (1/3)*miradaUserN(t1)+(1/3)*miradaUserN(t2) + (1/3)*miradaUserN(t3)

def miradaUserN(t):
    if(t >= 0 and t <= (1/3)):
        return 3*t
    elif (t > (1/3) and t <= 1):
        return 1
    else: 
        return 0

def tiempoTotalMirada(T):
    if(T>= 0 and T <= (1/4)):
        return 4*T
    elif(T>1/4 and T <= (3/4)):
        return 1
    elif(T>3/4 and T <= 1):
        return 4*(1-T)
    else:
        return 0

def silencioScore(t, best):
    return gaussian(1, best, 0.0025, t)

def gaussian(a,b,c, x):
    return a*math.exp( -(math.pow(x-b, 2)) /(2*c))


# Datos de entrada: Tiempo de mirada a cada usuario t1, t2, t3, 
# tiempo total de mirata tM, tiempo de silencio ts, tiempo total de prueba T

def PRCSformula(t1, t2, t3, tM, ts, T):
    audienceScore = miradaAUsuarios(t1/tM, t2/tM, t3/tM)
    tiempoMiradaScore = tiempoTotalMirada(tM/T)
    silencio = silencioScore(ts/T, 0.155)
    return 0.3*audienceScore + 0.3*tiempoMiradaScore + 0.3 *silencio


res = PRCSformula(float(sys.argv[1]), float(sys.argv[2]), float(sys.argv[3]), float(sys.argv[4]), float(sys.argv[5]), float(sys.argv[6]))
PRCS_res = (res*50)+12
print(PRCS_res)
#print(silencio(float(sys.argv[1]), 0.155 ))