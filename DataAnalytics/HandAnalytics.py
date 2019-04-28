import matplotlib as mpl
import matplotlib.pyplot as plt
import numpy as np


def DrawHandAnalytics(floatArray, interval):
    if(len(floatArray) < 2):
        print ("Invalid data array")
        return None
    xArray = np.abs(np.array(floatArray[2:][::2]))
    yArray = np.abs(np.array(floatArray[3:][::2]))
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

    coeficiente_variacion_x = desviacionX/mediaX
    coeficiente_variacion_y = desviacionY/mediaY

    print("El coeficiente de variacion X fué: ", (coeficiente_variacion_x*100), '%')
    print("El coeficiente de variacion Y fué: ", (coeficiente_variacion_y*100), '%')


    fig = plt.figure()
    plt.plot(steps, xArray, '-', color= 'blue', label = 'Eje X')
    plt.plot(steps, yArray, '-', color = 'red', label = 'Eje Y')


    plt.plot(steps, [desviacionX for x in steps], '-', label = "desviavion tipica X", color = 'red')
    plt.plot(steps, [mediaX for x in steps], '-', label = 'media X', color= 'green')
    plt.plot(steps, [medianaX for x in steps], '-', label = 'mediana X', color = 'orange')

    plt.plot(steps, [desviacionY for x in steps], '-', label = "desviavion tipica Y", color = 'red')
    plt.plot(steps, [mediaY for x in steps], '-', label = 'media Y', color= 'green')
    plt.plot(steps, [medianaY for x in steps], '-', label = 'mediana Y', color = 'orange')


    plt.ylabel('Valor de la mano por coordenada')
    plt.legend()

    return fig


