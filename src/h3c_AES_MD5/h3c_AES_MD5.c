#include <stdint.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "md5/md5.h"
#include "aes.h"
#include "h3c_dict.h"
#include "h3c_AES_MD5.h"

/* ��С������ */
//��С�˼�⣬��� ENDIANNESS=='l' ��ΪС��
static union { char c[4]; unsigned long mylong; } endian_test = { { 'l', '?', '?', 'b' } };
#define ENDIANNESS ((char)endian_test.mylong)
//��С��ת��
#define BigLittleSwap32(A) ((((uint32_t)(A)&0xff000000)>>24)|\
	(((uint32_t)(A)&0x00ff0000)>>8)|\
	(((uint32_t)(A)&0x0000ff00)<<8)|\
	(((uint32_t)(A)&0x000000ff)<<24))
//void main()
//{
//	test();
//}

int test() {
	unsigned char encrypt_data[32] = { 0xcf, 0xfe, 0x64, 0x73, 0xd5, 0x73, 0x3b, 0x1f, 0x9e, 0x9a, 0xee, 0x1a, 0x6b, 0x76, 0x47, 0xc8, 0x9e, 0x27, 0xc8, 0x92, 0x25, 0x78, 0xc4, 0xc8, 0x27, 0x03, 0x34, 0x50, 0xb6, 0x10, 0xb8, 0x35 };
	unsigned char decrypt_data[32];
	unsigned char i;

	// decrypt
	h3c_AES_MD5_decryption(decrypt_data, encrypt_data);

	// print
	printf("encrypted string = ");
	for (i = 0; i<32; ++i) {
		printf("%x%x", ((int)encrypt_data[i] >> 4) & 0xf,
			(int)encrypt_data[i] & 0xf);
	}
	printf("\n");
	printf("decrypted string = ");
	for (i = 0; i<32; ++i) {
		printf("%x%x", ((int)decrypt_data[i] >> 4) & 0xf,
			(int)decrypt_data[i] & 0xf);
	}
	// the decrypt_data should be 8719362833108a6e16b08e33943601542511372d8d1fb1ab31aa17059118a6ba
	getchar();
	return 0;
}

//�ο� h3c_AES_MD5.md �ĵ��ж��㷨��˵��
int h3c_AES_MD5_decryption(unsigned char *decrypt_data, unsigned char *encrypt_data)
{
	const unsigned char key[16] = { 0xEC, 0xD4, 0x4F, 0x7B, 0xC6, 0xDD, 0x7D, 0xDE, 0x2B, 0x7B, 0x51, 0xAB, 0x4A, 0x6F, 0x5A, 0x22 };        // AES_BLOCK_SIZE = 16
	unsigned char iv1[16] = { 'a', '@', '4', 'd', 'e', '%', '#', '1', 'a', 's', 'd', 'f', 's', 'd', '2', '4' };        // init vector
	unsigned char iv2[16] = { 'a', '@', '4', 'd', 'e', '%', '#', '1', 'a', 's', 'd', 'f', 's', 'd', '2', '4' }; //ÿ�μ��ܡ����ܺ�IV�ᱻ�ı䣡�����Ҫ����IV������Ρ������ġ�����
	unsigned int length_1;
	unsigned int length_2;
	unsigned char tmp0[32];
	unsigned char sig[255];
	unsigned char tmp2[16];
	unsigned char tmp3[16];
	// decrypt
	AES128_CBC_decrypt_buffer(tmp0, encrypt_data, 32, key, iv1);
	memcpy(decrypt_data, tmp0, 16);
	length_1 = *(tmp0 + 5);
	get_sig(*(uint32_t *)tmp0, *(tmp0 + 4), length_1, sig);
	MD5Calc(sig, length_1, tmp2);

	AES128_CBC_decrypt_buffer(tmp3, tmp0 + 16, 16, tmp2, iv2);

	memcpy(decrypt_data + 16, tmp3, 16);

	length_2 = *(tmp3 + 15);
	get_sig(*(uint32_t *)(tmp3 + 10), *(tmp3 + 14), length_2, sig + length_1);
	if (length_1 + length_2>32)
	{
		memcpy(decrypt_data, sig, 32);
	}
	else
	{
		memcpy(decrypt_data, sig, length_1 + length_2);
	}
	MD5Calc(decrypt_data, 32, decrypt_data);//��ȡMD5ժҪ���ݣ�������浽ǰ16λ��
	MD5Calc(decrypt_data, 16, decrypt_data + 16);//��ǰһMD5�Ľ������һ��MD5���浽��16λ
	return 0;
}


// ���ұ�������������ֵ��ƫ�����Լ����Ȳ�������
char* get_sig(uint32_t index, int offset, int length, unsigned char* dst)
{
	uint32_t index_tmp;
	const unsigned char *base_address;
	// printf("index = %x\n" ,index);

	if (ENDIANNESS == 'l')
	{
		index_tmp = BigLittleSwap32(index); // С���������PC�ܹ�
	}
	else
	{
		index_tmp = index; // �������MIPS�ܹ�
	}
	printf("h3c_AES_MD5!\n");
	switch (index_tmp) // this line works in mips.
	{
	case 0x89D115D0:base_address = x89D115D0; break;
	case 0xC2BFCB45:base_address = xC2BFCB45; break;
	case 0x39264F3B:base_address = x39264F3B; break;
	default:
		printf("lookup dict failed.\n"); // ���ʧ��
		base_address = 0x89D115D0;
		break;
	}
	memcpy(dst, base_address + offset, length);
	return dst;
}