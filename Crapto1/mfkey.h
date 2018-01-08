//-----------------------------------------------------------------------------
// Merlok - June 2011
// Roel - Dec 2009
// Unknown author
//
// This code is licensed to you under the terms of the GNU GPL, version 2 or,
// at your option, any later version. See the LICENSE.txt file for the text of
// the license.
//-----------------------------------------------------------------------------
// MIFARE Darkside hack
//-----------------------------------------------------------------------------

#ifndef MFKEY_H
#define MFKEY_H

#include <stdio.h>
#include <stdint.h>
#include <stdbool.h>
#include "crapto1.h"

#ifdef __cplusplus
extern "C" {
#endif

typedef struct {
	uint32_t cuid;
	uint32_t nonce;
	uint32_t ar;
	uint32_t nr;
	uint32_t at;
	uint32_t nonce2;
	uint32_t ar2;
	uint32_t nr2;
	uint8_t  sector;
	uint8_t  keytype;
} nonces_t;

__declspec(dllexport) bool mfkey32(uint32_t cuid, uint32_t nt0, uint32_t nt1, uint32_t nr0, uint32_t ar0, uint32_t nr1, uint32_t ar1, uint64_t *outputkey);
__declspec(dllexport) bool mfkey32_moebius(uint32_t cuid, uint32_t nt0, uint32_t nt1, uint32_t nr0, uint32_t ar0, uint32_t nr1, uint32_t ar1, uint64_t *outputkey);
void mfkey64(nonces_t data, uint64_t *outputkey);

#ifdef __cplusplus
}
#endif
#endif
