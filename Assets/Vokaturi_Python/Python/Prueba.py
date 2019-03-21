import sys
import Vokaturi


class vokaNetWrapper:
	def __init__(self, DLLstring):	
		print("Loading library...")
		#Vokaturi.load("/DLL/OpenVokaturi-3-0-win32.dll")
		Vokaturi.load(DLLstring)
		print(Vokaturi.versionAndLicense())
		#return
	
	def vokalculate(self, floatArray):
		print(type(floatArray[0]))
		samplerate = 44100

		buffer_length = len(floatArray)
		cbuff = Vokaturi.SampleArrayC(buffer_length)

		voice = Vokaturi.Voice(samplerate, buffer_length)
		voice.fill(buffer_length, cbuff)

		quality = Vokaturi.Quality()
		emotionProbabilities = Vokaturi.EmotionProbabilities()
		voice.extract(quality, emotionProbabilities)

		
		if quality.valid:
			print ("Neutral: %.3f" % emotionProbabilities.neutrality)
			print ("Happy: %.3f" % emotionProbabilities.happiness)
			print ("Sad: %.3f" % emotionProbabilities.sadness)
			print ("Angry: %.3f" % emotionProbabilities.anger)
			print ("Fear: %.3f" % emotionProbabilities.fear)
		else:
			print ("Not enough sonorancy to determine emotions")

		voice.destroy()


		return {
		"Neutral": emotionProbabilities.neutrality,
		"Happy": emotionProbabilities.happiness,
		"Sad": emotionProbabilities.sadness,
		"Angry": emotionProbabilities.anger,
		"Fear": emotionProbabilities.fear
		}




