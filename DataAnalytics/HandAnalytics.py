import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np


def DrawHandAnalytics(floatArray, interval):
    if(len(floatArray) < 2):
        print ("Invalid data array")
        return None
    xArray = np.abs(np.array(floatArray[3:][::3]))
    yArray = np.abs(np.array(floatArray[4:][::3]))
    zArray = np.abs(np.array(floatArray[5:][::3]))

    steps = np.zeros(len(xArray))
    i = 0

    for z in range(len(xArray)):
        if(i > 0):
            steps[i] = steps[i-1]+ interval
        i = i +1


    desviacionX = np.std(xArray)
    mediaX = np.mean(xArray)
    medianaX = np.median(xArray)

    desviacionY = np.std(yArray)
    mediaY = np.mean(yArray)
    medianaY = np.median(yArray)

    
    desviacionZ = np.std(zArray)
    mediaZ = np.mean(zArray)
    medianaZ = np.median(zArray)

    coeficiente_variacion_x = desviacionX/mediaX
    coeficiente_variacion_y = desviacionY/mediaY
    coeficiente_variacion_z = desviacionZ/mediaZ


    print("\n-------Eje X --------")
    print("Media: ", mediaX)
    print("Mediana: ", medianaX)
    print("Desviación Típica: ", desviacionX)
    print("El coeficiente de variacion en eje X: ", (coeficiente_variacion_x*100), '%')
    print("\n")

    print("\n-------Eje Y --------")
    print("Media: ", mediaY)
    print("Mediana: ", medianaY)
    print("Desviación Típica: ", desviacionY)
    print("El coeficiente de variacion en eje Z: ", (coeficiente_variacion_y*100), '%')
    print("\n")
    
    print("\n-------Eje Z --------")
    print("Media: ", mediaZ)
    print("Mediana: ", medianaZ)
    print("Desviación Típica: ", desviacionZ)
    print("El coeficiente de variacion en eje Z: ", (coeficiente_variacion_z*100), '%')
    print("\n")


    fig = plt.figure()
    plt.plot(steps, xArray, '-', color= 'blue', label = 'Eje X')
    plt.plot(steps, yArray, '-', color = 'red', label = 'Eje Y')
    plt.plot(steps, zArray, '-', color = 'green', label = 'Eje Z')
    plt.ylim(0, 1)
    plt.ylabel('Velocidad')
    plt.xlabel('Tiempo')

    plt.legend()
    return fig


