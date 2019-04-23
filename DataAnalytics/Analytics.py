import numpy as np

import SoundLoudness


floatArr = np.random.rand(1024)
step = 0.1

SoundLoudness.DrawSoundLoudness(floatArr, step)