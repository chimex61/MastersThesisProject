PROJECT(currennt)

CMAKE_MINIMUM_REQUIRED (VERSION 2.8)

add_definitions( -DBOOST_ALL_NO_LIB )
# add_definitions( -DBOOST_ALL_DYN_LINK )
set( Boost_USE_STATIC_LIBS ON ) # or Off, depending on what you want

FIND_PACKAGE (CUDA)
FIND_PACKAGE (Boost 1.48.0 REQUIRED COMPONENTS program_options system filesystem random thread  REQUIRED)

INCLUDE_DIRECTORIES (${Boost_INCLUDE_DIR})

LINK_DIRECTORIES (${Boost_LIBRARY_DIR})

SET (CMAKE_BUILD_TYPE Release)


FILE (GLOB_RECURSE src_lib     currennt_lib/*.cpp currennt_lib/*.hpp     currennt_lib/*.h     currennt_lib/*.cu     currennt_lib/*.cuh)
FILE (GLOB_RECURSE src_trainer currennt/*.cpp     currennt/*.hpp         currennt/*.h         currennt/*.cu         currennt/*.cuh)

CUDA_ADD_EXECUTABLE (${PROJECT_NAME} ${src_lib} ${src_trainer})

CUDA_ADD_CUBLAS_TO_TARGET (${PROJECT_NAME})
TARGET_LINK_LIBRARIES (${PROJECT_NAME} netcdf)
TARGET_LINK_LIBRARIES (${PROJECT_NAME} ${Boost_LIBRARIES})

