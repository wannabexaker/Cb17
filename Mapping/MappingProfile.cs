using System;
using AutoMapper;
using CB17.Models;
using CB17.DTOs;

namespace CB17.Mapping;

// Central place where we configure mappings between Entities and DTOs
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // =======================
        // Candidate mappings
        // =======================

        // Entity → DTO (response)
        CreateMap<Candidate, CandidateDto>();

        // Create DTO → Entity (POST)
        CreateMap<CandidateCreateDto, Candidate>();

        // Update DTO → Entity (PUT)
        CreateMap<CandidateUpdateDto, Candidate>();

        // =======================
        // Certification mappings
        // =======================

        // Entity → DTO (response)
        CreateMap<Certification, CertificationDto>();

        // Create DTO → Entity (POST)
        CreateMap<CertificationCreateDto, Certification>();

        // Update DTO → Entity (PUT)
        CreateMap<CertificationUpdateDto, Certification>();
    }
}
