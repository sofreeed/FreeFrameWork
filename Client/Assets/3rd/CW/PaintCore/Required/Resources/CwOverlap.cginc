float CW_GetOverlapStrength_Add(float strength, float strengthClip)
{
	return max(0, strength - strengthClip);
}

float CW_GetOverlapStrength_Lin(float strength, float strengthClip)
{
	if (strength > strengthClip && strengthClip < 1.0f)
	{
		return 1.0f - (1.0f - strength) / (1.0f - strengthClip);
	}
	else
	{
		return 0.0f;
	}
}

float CW_GetOverlapStrength(float strength, float strengthClip)
{
	#if BLEND_MODE_INDEX == 3 // ADDITIVE
		return CW_GetOverlapStrength_Add(strength, strengthClip);
	#elif BLEND_MODE_INDEX == 5 // SUBTRACTIVE
		return CW_GetOverlapStrength_Add(strength, strengthClip);
	#else
		return CW_GetOverlapStrength_Lin(strength, strengthClip);
	#endif
}