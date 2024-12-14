﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos;

public record CreateGameDto(
    [Required]
    [StringLength(50)]
    string Name,
    
    [Required]
    int GenreId,
    
    [Required]
    [Range(1, 100)]
    decimal Price,
    
    DateOnly ReleaseDate
);