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


    print("El coeficiente de variacion X fué: ", (coeficiente_variacion_x*100), '%')
    print("El coeficiente de variacion Y fué: ", (coeficiente_variacion_y*100), '%')
    print("El coeficiente de variacion Z fué: ", (coeficiente_variacion_z*100), '%')



    fig = plt.figure()
    plt.plot(steps, xArray, '-', color= 'blue', label = 'Eje X')
    plt.plot(steps, yArray, '-', color = 'red', label = 'Eje Y')
    plt.plot(steps, zArray, '-', color = 'green', label = 'Eje Z')



    plt.plot(steps, [desviacionX for x in steps], '-', label = "desviavion tipica X", color = 'red')
    plt.plot(steps, [mediaX for x in steps], '-', label = 'media X', color= 'brown')
    plt.plot(steps, [medianaX for x in steps], '-', label = 'mediana X', color = 'orange')

    plt.plot(steps, [desviacionY for x in steps], '-', label = "desviavion tipica Y", color = 'yellow')
    plt.plot(steps, [mediaY for x in steps], '-', label = 'media Y', color= 'pink')
    plt.plot(steps, [medianaY for x in steps], '-', label = 'mediana Y', color = 'purple')
    
    plt.plot(steps, [desviacionZ for x in steps], '-', label = "desviavion tipica Z", color = 'black')
    plt.plot(steps, [mediaZ for x in steps], '-', label = 'media Z', color= 'gray')
    plt.plot(steps, [medianaZ for x in steps], '-', label = 'mediana Z', color = 'beige')


    plt.ylabel('Valor de la mano por coordenada')

    plt.legend()
    return fig


